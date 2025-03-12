Shader "Custom/Custom_Default"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ShadowHardness ("Shadow Hardness", Float) = 1
        _VertEffectSize ("Vert Snap Strength", Float) = 128
    }
    SubShader
    {
        Pass
        {
            Tags {
                "Queue"="Geometry"
                "RenderPipeline"="UniversalPipeline"
                "LightMode" = "UniversalForward"
            }

            LOD 100

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #pragma multi_compile_instancing
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_SCREEN
            
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
                float4 normal : NORMAL;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
                float4 worldPos : TEXCOORD1;
            };
            
            #define VERT_EFFECT_SIZE 128.0

            sampler2D _MainTex;
            
            CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;
            float _ShadowHardness;
            CBUFFER_END
            

            v2f vert (appdata IN)
            {
                v2f o;
                
                //vertice afetado pela matriz MVP normal
                float4 transfVertex = mul(UNITY_MATRIX_MVP, IN.vertex);
                o.vertex = transfVertex;      

                //aplica perspectiva  
                o.vertex.xyz = transfVertex.xyz / transfVertex.w;    

                //snap das posicoes x e y de acordo com a aspect ratio (0.5625 pra 16/9)
                o.vertex.x = floor(o.vertex.x * VERT_EFFECT_SIZE) / VERT_EFFECT_SIZE;   
                o.vertex.y = floor(o.vertex.y * (VERT_EFFECT_SIZE*0.5625)) / (VERT_EFFECT_SIZE*0.5625);

                //desaplica perspectiva (pq se nao acontece 2x kk)
                o.vertex.xyz *= transfVertex.w; 

                o.vertex = mul(UNITY_MATRIX_I_VP, o.vertex);
                o.worldPos = o.vertex;
                o.vertex = mul(UNITY_MATRIX_VP, o.vertex);

                o.normal = IN.normal;

                o.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                
                return o;
            }

            //calculo de incidencia da luz
            float3 CalculateLight(Light light, float4 normal)
            {
                float diffuse = saturate(dot(normal, light.direction)*_ShadowHardness);

                return light.color * diffuse * light.shadowAttenuation;
            }

            float4 frag (v2f IN) : SV_Target
            {
                float4 col = 0;

                //Sampling sombras
                float4 shadowCoord = TransformWorldToShadowCoord(IN.worldPos);

                //Calculo de luz principal
                Light mainLight = GetMainLight(shadowCoord);
                col.rgb = tex2D(_MainTex, IN.uv).rgb * CalculateLight(mainLight, IN.normal);
                uint pixelLightCount = GetAdditionalLightsCount();

                //Loop de luz
                LIGHT_LOOP_BEGIN(pixelLightCount)

                //pega a luz
                lightIndex = GetPerObjectLightIndex(lightIndex);
                Light light = GetAdditionalPerObjectLight(lightIndex, IN.worldPos);

                //pega a sombra
                light.shadowAttenuation = AdditionalLightRealtimeShadow(lightIndex, IN.worldPos, light.direction);
                float atten = light.distanceAttenuation * light.shadowAttenuation;
                
                col.rgb += light.color;// * atten;

                LIGHT_LOOP_END

                return col;
            }

            

            ENDHLSL
        }


        //ShadowCaster
        Pass
        {
            Tags{"Lightmode"="ShadowCaster"}
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            struct appdata
            {
                float4 vertex : POSITION;
            };
            
            struct v2f
            {
                float4 vertex : SV_POSITION;
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
                
                return o;
            }

            float4 frag (v2f IN) : SV_Target
            {
                return 0;
            }
            ENDHLSL
        }
    }
}
