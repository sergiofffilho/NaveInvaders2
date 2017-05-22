// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "CloverSwatch/AddEfect"
{
		Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Color) = (0,0,0,0)
	}

	SubShader
	{
	ZWrite Off
	Blend One One 
	Cull Off
		Tags
		{
			"RenderType"="Transparent"
			"Queue"="Transparent"
			"IgnoreProjector"="True" 
		}

		Pass{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct appdata{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
					float3 normal : NORMAL;
					
				
				};
				struct v2f{
					float2 uv : TEXCOORD0;
					float2 screenuv : TEXCOORD1;
					float4 vertex : SV_POSITION;
					float3 objectPos : TEXCOORD3;
					float depth : DEPTH;
				
				};

				float4 _MainTex_ST;
				sampler2D _MainTex;

				v2f vert (appdata v){

					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);

					o.screenuv = ((o.vertex.xy / o.vertex.w) + 1)/2;
					o.screenuv.y = 1 - o.screenuv.y;

					o.depth = -mul(UNITY_MATRIX_MV, v.vertex).z *_ProjectionParams.w;

					o.objectPos = v.vertex.xyz;		

					return o;
				}

			fixed4 _Color;

			fixed4 texColor(v2f i, float rim)
			{
				fixed4 mainTex = tex2D(_MainTex, i.uv);
			
				return mainTex * _Color;
			}
				fixed4 frag (v2f i) : SV_Target
				{
					fixed4 hexes = tex2D(_MainTex, i.uv)*_Color;
					fixed4 col = _Color*_Color.a+hexes  ;
					return col;
				}
				ENDCG
		}
	}
}