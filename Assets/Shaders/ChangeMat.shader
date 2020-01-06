Shader "Custom/ChangeMaterial" {
	Properties{
		_Color("Tint Color", Color) = (.9, .9, .9, 1.0)
		_MainTex("Base (RGB)", 2D) = "white" {}
		_SecondaryTex("Base (RGB)", 2D) = "white" {}
		_Blend("Blend", Range(0.0,1.0)) = 0.0
	}
		SubShader{
			Tags {"RenderType" = "Opaque"}
			LOD 200

			Pass{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				uniform float4 _Color;
		uniform float4 _LightColor0;
				struct vertexInput {
					float4 vertex : POSITION;
					float3 normal : NORMAL;
					fixed2 uv : TEXCOORD0;
				};

				struct vertexOutput {
					float4 pos : SV_POSITION;
					float4 col : COLOR;
					fixed2 uv : TEXCOORD0;
				};

				sampler2D _MainTex;
				sampler2D _SecondaryTex;

				fixed4 _MainTex_ST;
				fixed4 _SecondaryTex_ST;

				float _Blend;

				vertexOutput vert(vertexInput input) {
					vertexOutput output;

					float3 normalDirection = UnityObjectToWorldNormal(input.normal);
					float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);

					float3 diffuseReflection = _LightColor0.rgb * _Color.rgb
						* max(0.0, dot(normalDirection, lightDirection));
					output.col = float4(diffuseReflection, 1.0);
					output.pos = UnityObjectToClipPos(input.vertex);
					output.uv = TRANSFORM_TEX(input.uv, _MainTex);
					return output;
				}

				fixed4 frag(vertexOutput v) : COLOR{
				fixed4 col = lerp(tex2D(_MainTex, v.uv),tex2D(_SecondaryTex, v.uv),_Blend) * v.col;
				return col;
				}
					ENDCG
			}
		}
}