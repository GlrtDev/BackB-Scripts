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
			 Tags {"LightMode" = "ForwardBase"}

			CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				//#include "Lighting.cginc"
				#include "AutoLight.cginc"
				#pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight

				uniform float4 _Color;
				uniform float4 _LightColor0;
				struct vertexInput {
					float4 vertex : POSITION;
					float3 normal : NORMAL;
					fixed2 uv : TEXCOORD0;
					
				};

				struct vertexOutput {
					float4 pos : SV_POSITION;
					float3 diff : COLOR0;
					fixed3 ambient : COLOR1;
					fixed2 uv : TEXCOORD0;
					SHADOW_COORDS(6)
				};

				sampler2D _MainTex;
				sampler2D _SecondaryTex;

				fixed4 _MainTex_ST;
				fixed4 _SecondaryTex_ST;

				float _Blend;

				vertexOutput vert(vertexInput v) {
					vertexOutput output;
					
					//output.col = _Color;
					output.pos = UnityObjectToClipPos(v.vertex);
					output.uv = TRANSFORM_TEX(v.uv, _MainTex);
					half3 worldNormal = UnityObjectToWorldNormal(v.normal);

					half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
					output.diff =  nl * _LightColor0.rgb;
					output.ambient = ShadeSH9(half4(worldNormal, 1));
					TRANSFER_SHADOW(output)
					return output;
				}

				fixed4 frag(vertexOutput input) : SV_Target{

					fixed4 col = lerp(tex2D(_MainTex, input.uv),tex2D(_SecondaryTex, input.uv),_Blend);
					fixed shadow = SHADOW_ATTENUATION(input);
					fixed3 lighting = input.diff * shadow + input.ambient;
					col.rgb *= lighting;
					return col;
				}
				ENDCG
			}
		
		}
		//FallBack "Diffuse"
}