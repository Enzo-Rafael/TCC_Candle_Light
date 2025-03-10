Shader "Custom/Custom_Default"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags {
            "Queue"="Geometry"
            "RenderPipeline"="UniversalPipeline"
        }
        LOD 100
        
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float4 normal : NORMAL;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };
            
            #define VERT_EFFECT_SIZE 128.0

            sampler2D _MainTex;
            
            CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;

            CBUFFER_END
            

            v2f vert (appdata IN)
            {
                v2f o;
                
                //vertice afetado pela matriz MVP normal
                float4 transfVertex = mul(UNITY_MATRIX_MVP, IN.vertex);
                o.vertex = transfVertex;      

                o.vertex.xyz = transfVertex.xyz / transfVertex.w; //aplica perspectiva      

                //snap das posicoes x e y de acordo com a aspect ratio (0.5625 pra 16/9)
                o.vertex.x = floor(o.vertex.x * VERT_EFFECT_SIZE) / VERT_EFFECT_SIZE;   
                o.vertex.y = floor(o.vertex.y * (VERT_EFFECT_SIZE*0.5625)) / (VERT_EFFECT_SIZE*0.5625);
                
                o.vertex.xyz *= transfVertex.w; //desaplica perspectiva (pq se nao acontece 2x kk)

                o.color.rgb = _GlossyEnvironmentColor.rgb * dot(IN.normal, float3(0,1,0))+0.5;

                o.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                
                return o;
            }

            float4 frag (v2f IN) : SV_Target
            {
                float4 col = tex2D(_MainTex, IN.uv) * IN.color;
                return col;
            }
            ENDHLSL
        }
    }
}
