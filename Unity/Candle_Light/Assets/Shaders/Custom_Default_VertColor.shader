Shader "Custom/Custom_Default_VertColor"
{
    Properties
    {
        _ShadowTint ("Shadow Tint", Color) = (0.5,0.5,0.5)
        _VertEffectSize ("Vert Snap Strength", Float) = 128
    }
    SubShader
    {
        Pass
        {
            Name "ForwardPass"

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
            #pragma multi_compile _ _FORWARD_PLUS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_SCREEN
            #pragma multi_complie _ _ADITIONAL_LIGHTS
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RealTimeLights.hlsl"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float4 normal : NORMAL;
                float4 color : COLOR;
            };
            
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normalWS : NORMAL;
                float4 color : COLOR;
                float3 worldPos : TEXCOORD1;
            };
            
            #define VERT_EFFECT_SIZE 128.0
            #define SHADOW_HARDNESS 10
            #define LIGHT_ATTENUATION 50
            
            CBUFFER_START(UnityPerMaterial)
            half3 _ShadowTint;
            float _ShadowHardness;
            CBUFFER_END
            

            v2f vert (appdata IN)
            {
                v2f o;
                 
                // Vertice afetado pela matriz MVP normal
                float4 transfVertex = mul(UNITY_MATRIX_MVP, IN.vertex);
                o.vertex = transfVertex;      

                // Aplica perspectiva  
                o.vertex.xyz = transfVertex.xyz / transfVertex.w;    

                // Snap das posicoes x e y de acordo com a aspect ratio (0.5625 pra 16/9)
                o.vertex.x = floor(o.vertex.x * VERT_EFFECT_SIZE) / VERT_EFFECT_SIZE;   
                o.vertex.y = floor(o.vertex.y * (VERT_EFFECT_SIZE*0.5625)) / (VERT_EFFECT_SIZE*0.5625);

                // Desaplica perspectiva (pq se nao acontece 2x kk)
                o.vertex.xyz *= transfVertex.w; 

                o.vertex = mul(UNITY_MATRIX_I_VP, o.vertex);
                o.worldPos = o.vertex;
                o.vertex = mul(UNITY_MATRIX_VP, o.vertex);

                o.normalWS = TransformObjectToWorldNormal(IN.normal);
                
                o.color = IN.color;

                return o;
            }

            // Calculo de incidencia da luz
            half CalculateLight(Light light, float3 normal)
            {
                half diffuse = saturate(dot(normal, light.direction) * SHADOW_HARDNESS);

                return diffuse * light.shadowAttenuation;
            }

            // Calculo de luz sem normal
            half CalculateSecondLight(Light light)
            {
                half diffuse = (light.distanceAttenuation * LIGHT_ATTENUATION);

                return diffuse;
            }

            half4 frag (v2f IN) : SV_Target
            {
                half4 col = 0;
                half lightVal = 0;

                // Sampling sombras
                half4 shadowCoord = TransformWorldToShadowCoord(IN.worldPos);

                // Calculo de luz principal
                Light light = GetMainLight(shadowCoord);
                lightVal = CalculateLight(light, IN.normalWS);

                // Preparacao para loop de luz
                InputData inputData = (InputData)0;
                inputData.positionWS = IN.worldPos;
                inputData.positionCS = IN.vertex;
                inputData.normalWS = IN.normalWS;
                inputData.viewDirectionWS = GetWorldSpaceNormalizeViewDir(IN.worldPos);

                // Calculo de luzes secundarias
                uint pixelLightCount = GetAdditionalLightsCount();
                LIGHT_LOOP_BEGIN(pixelLightCount)
                    Light additionalLight = GetAdditionalLight(lightIndex, inputData.positionWS, 1);
                    lightVal += CalculateSecondLight(additionalLight);
                LIGHT_LOOP_END

                lightVal = saturate(lightVal);
                col = IN.color;

                col.rgb = (col * lightVal) + (col * _ShadowTint * (1-lightVal));
                

                return col;
            }

            

            ENDHLSL
        }


        // ShadowCaster
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
            
            #define VERT_EFFECT_SIZE 256.0

            sampler2D _MainTex;
            
            CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;

            CBUFFER_END
            

            v2f vert (appdata IN)
            {
                v2f o;

                // Nao ta dando certo essa parte, deixa o shadowcaster normal por enquanto

                //// Vertice afetado pela matriz MVP da camera
                //float4 transfVertex = mul(UNITY_MATRIX_M, IN.vertex);
                //transfVertex = mul(unity_WorldToCamera, IN.vertex);
                //transfVertex = mul(unity_CameraProjection, transfVertex);

                //o.vertex = transfVertex;      

                ////o.vertex.xyz = transfVertex.xyz / transfVertex.w; //aplica perspectiva      

                //// Snap das posicoes x e y de acordo com a aspect ratio (0.5625 pra 16/9)
                //o.vertex.x = floor(o.vertex.x * VERT_EFFECT_SIZE) / VERT_EFFECT_SIZE;   
                //o.vertex.y = floor(o.vertex.y * (VERT_EFFECT_SIZE*0.5625)) / (VERT_EFFECT_SIZE*0.5625);
                //
                ////o.vertex.xyz *= transfVertex.w; // Desaplica perspectiva (pq se nao acontece 2x kk)

                //// Desaplica a perspectiva da camera
                //o.vertex = mul(unity_CameraInvProjection, o.vertex);
                //o.vertex = mul(unity_CameraToWorld, o.vertex);

                o.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);

                return o;
            }

            float4 frag (v2f IN) : SV_Target
            {
                return 1;
            }
            ENDHLSL
        }
    }
}
