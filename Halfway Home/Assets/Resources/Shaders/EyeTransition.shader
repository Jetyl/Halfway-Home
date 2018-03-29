Shader "UI/EyeTransition"
{
	Properties
	{
		[PerRendererData] _MainTex ("Texture", 2D) = "white" {}
		[PerRendererData] _Progress ("Transition Progress", Range(0.0, 1.0)) = 0.0
		[PerRendererData] _Width ("Fade Width", Range(0.00001, 1.0)) = 1.0
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
		Cull Off

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
			float4 _Flip;
			
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
				float2 ndc = uv * 2.0f - 1.0f;

				float startEyeTime = 0.35f;

				float startupFade = 1.0f - clamp((startEyeTime - _Progress) / startEyeTime, 0.0f, 1.0f);

				//Starts at 
				float eyeCloseAmount = _Progress;//clamp((_Progress - startEyeTime) / (1.0f - startEyeTime), 0.0f, 1.0f);

				float yScalarVal = pow(eyeCloseAmount, 8.0f);

				ndc.y *= 1.0f + 1000.0f * yScalarVal;

				float len = length(ndc);// / ndcScalar;

				//len = clamp(len, 0.0f, 1.0f);// *ndcScalar;
				float innerFade = 1.0f - pow(clamp(len, 0.0f, 1.0f), 4.0f);

				float visible = (sign(1.0f - len) + 1.0f) * 0.5f;

				float fade = visible * innerFade;

				return lerp(1.0f, fade, startupFade);
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
