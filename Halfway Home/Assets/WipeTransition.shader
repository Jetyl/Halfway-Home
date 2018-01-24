Shader "UI/WipeTransition"
{
	Properties
	{
		[PerRendererData] _MainTex ("Texture", 2D) = "white" {}
		[PerRendererData] _Progress ("Transition Progress", Range(0.0, 1.0)) = 0.0
		[PerRendererData] _Width ("Fade Width", Range(0.00001, 1.0)) = 1.0
		[PerRendererData] _Dir ("Fade Direction (only X and Y are used)", Vector) = (1.0, 0.0, 0.0, 0.0)
	}
	SubShader
	{
		Tags { 
			"RenderType"="Transparent" 
			"Queue"="Transparent+1" 
			"PreviewType"="Plane"
			"IgnoreProjector"="True"
			"CanUseSpriteAtlas"="True"
		}
		LOD 100

		Blend SrcAlpha OneMinusSrcAlpha

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

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Progress;
			float _Width;
			float4 _Dir;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			//get that gud fade boiiiiiiiiiii
			float GetFade(float2 uv)
			{
				float2 flip;
				flip.x = 1.0f - (sign(_Dir.x) + 1.0f) * 0.5f;
				flip.y = 1.0f - (sign(_Dir.y) + 1.0f) * 0.5f;

				//Flip the UVs to the other side if the direction is negative
				uv.x = lerp(uv.x, 1.0f - uv.x, flip.x);
				uv.y = lerp(uv.y, 1.0f - uv.y, flip.y);

				//Normalize the fade direction cuz we gotta
				float2 fadeDir = abs(normalize(_Dir.xy));

				//To compensate for the fact that a square is longer along the diagonal, we gotta increase the max length
				float minDim = min(fadeDir.x, fadeDir.y);
				float maxDim = max(fadeDir.x, fadeDir.y);

				float h = minDim / maxDim;
				float lengthScalar = sqrt(1.0f + h * h);

				float maxLen = (1.0f + _Width) * lengthScalar;

				//The actual progress value we use 
				float progVal = _Progress * maxLen;

				//Get how far along the direction this pixel's UV is
				float distance = dot(uv, fadeDir);

				//Binary limit value so we don't fade out in both directions
				float limit = (sign(distance - progVal) + 1.0f) * 0.5f;

				float fade = 1.0f - min(abs(progVal - distance) / _Width, 1.0f);

				return lerp(fade, 1.0f, limit);
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);

				col.w *= GetFade(i.uv);
				
				return col;
			}
			ENDCG
		}
	}
}
