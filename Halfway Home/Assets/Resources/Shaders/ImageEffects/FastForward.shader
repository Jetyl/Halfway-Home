Shader "Post/FastForward"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _SmallTex;
			float4 _MainTex_TexelSize;
			float4 _SmallTex_TexelSize;

			float _intensity;
			float _colorBleedDistortAmount;
			float _curvyDistortionSpeed;
			float _curvyDistortion;
			float _scanlineDistortion;
			float _colorDull;
			float _vhsLineIntensity;
			int _blurRadius;

			float4 BlurSample(sampler2D image, float2 uv, float2 pixelSize, int radius)
			{
				float4 total = float4(0.0f, 0.0f, 0.0f, 0.0f);
				float2 xOffset = float2(pixelSize.x, 0.0f);
				float2 yOffset = float2(0.0f, pixelSize.y);

				int totalWidth = 1 + 2 * radius;
				int numSamples = totalWidth * totalWidth;

				[unroll(10)]
				for (int x = -radius; x <= radius; x++)
				{
					[unroll(10)]
					for (int y = -radius; y <= radius; y++)
					{
						total += tex2D(image, uv + xOffset * x + yOffset * y);
					}
				}

				return total / numSamples;
			}

			float4 VHSSample(sampler2D image, float2 uv)
			{
				//From wikipedia
				//cuz i can never remember these constants
				float3 colorBrightness = float3(0.2126f, 0.7152f, 0.0722f);

				float4 self = tex2D(image, uv);
				float2 uvOffset = float2(_SmallTex_TexelSize.x, 0.0f) * _colorBleedDistortAmount;
				//float4 b = tex2D(_SmallTex, uv + uvOffset);
				//float4 g = tex2D(_SmallTex, uv + uvOffset * 1.5f);
				//float4 r = tex2D(_SmallTex, uv + uvOffset * 2.3f);

				float4 b = BlurSample(_SmallTex, uv - uvOffset * 0.3f, _SmallTex_TexelSize.xy, _blurRadius);
				float4 g = BlurSample(_SmallTex, uv - uvOffset * 1.0f, _SmallTex_TexelSize.xy, _blurRadius);
				float4 r = BlurSample(_SmallTex, uv - uvOffset * 4.3f, _SmallTex_TexelSize.xy, _blurRadius);
				float4 w = float4(1.0f, 1.0f, 1.0f, 1.0f);

				float dull = saturate(_colorDull);

				b = lerp(w, b, dull);
				g = lerp(w, g, dull);
				r = lerp(w, r, dull);

				float selfGray = dot(self.xyz, colorBrightness) * (dull * 0.4f + 0.6f);

				return float4(r.x * selfGray, g.y * selfGray, b.z * selfGray, self.w);
			}

			float rand(float2 ST)
			{
				return frac(sin(dot(ST.xy, float2(12.9898f, 78.233f))) * 43758.5453f);
			}

			float GetBadLine(float t)
			{
				return pow(abs((sin(t * 3.1f) + sin(23.0f * t + 3.1436f)) * 0.5f), 64.0f);
			}

			float2 GetDistortedUV(float2 uv)
			{
				float4 uvScalars = float4(16.28f, 310.0f, 130.0f, 900.0f);
				float4 timeScalars = float4(2.0f, 3.0f, 13.0f, 46.0f);

				float4 tVals = uv.y * uvScalars + _Time.y * timeScalars * _curvyDistortionSpeed;

				float4 sines = float4(sin(tVals.x), sin(tVals.y), sin(tVals.z), sin(tVals.w));

				float combinedSine = sines.x * 0.05f + 0.05f * sines.y + 0.05f * sines.z + 0.05f * sines.w;

				return uv + float2(combinedSine * _curvyDistortion, 0.0f);
			}

			fixed4 frag (v2f i) : SV_Target
			{
				float2 blitSize = float2(5.0f, 300.0f);

				float2 roundedUV = i.uv * blitSize;
				//roundedUV = floor(roundedUV) / blitSize;

				roundedUV.x += sin(_Time.y * 3.0f + i.uv.y * 1.3f) * 0.1f;
				roundedUV.y += sin(_Time.y * 3.0f + i.uv.y * 1.3f) * 50.0f;

				roundedUV = floor(roundedUV) / blitSize;

				float4 badLines;

				badLines.x = GetBadLine(_Time.y + i.uv.y);
				badLines.y = GetBadLine(_Time.y * 2.3f - i.uv.y * 2.3f);
				badLines.z = GetBadLine(_Time.y * 7.2f - i.uv.y * 1.3f + 3.2f);
				badLines.w = GetBadLine(_Time.y * 1.67f + i.uv.y * 4.3f - 0.354f);

				float totalBadLine = badLines.x + badLines.y + badLines.z + badLines.w;

				float vhsStatic = rand(roundedUV);

				vhsStatic = pow(vhsStatic, 4.0f);

				float totalVHSLineStatic = vhsStatic * totalBadLine * _vhsLineIntensity;

				float distortOffset = -(sin(_Time.y * 3.0f + i.uv.y * 3.14159f) * 0.5f);

				distortOffset = floor(distortOffset * 2.0f) / 2.0f;

				float2 distortedUV = GetDistortedUV(i.uv);

				float staticYes = rand(i.uv + _Time.y);

				float sIntensity = saturate(_intensity);

				distortedUV = lerp(i.uv, distortedUV + float2(distortOffset * 0.05f * _scanlineDistortion, 0.0f), sIntensity);

				float4 c = VHSSample(_MainTex, distortedUV);

				float4 unmodified = tex2D(_MainTex, distortedUV);

				//return c + totalVHSLineStatic + staticYes * 0.05f;

				return lerp(unmodified, c + totalVHSLineStatic + staticYes * 0.05f, sIntensity);
			}
			ENDCG
		}
	}
}
