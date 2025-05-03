Shader "Custom/Custom_Transparent"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Tint ("Shadow Tint", Color) = (0.5,0.5,0.5)
        _Alpha ("Alpha", Float) = 0.5
        _FadeStrength ("Fade by Proximity", Float) = 10
        [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull", Float) = 0
    }
    SubShader
    {
        Pass
        {
            Name "ForwardPass"
            ZWrite ON
            Cull [_Cull]
            Blend SrcAlpha OneMinusSrcAlpha

            Tags {
                "Queue"="Transparent"
                "RenderPipeline"="UniversalPipeline"
                "LightMode" = "UniversalForward"
            }

            LOD 0

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile_instancing
            #pragma multi_compile _ _FORWARD_PLUS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile_fragment _ _SHADOWS_SOFT _SHADOWS_SOFT_LOW
            #pragma multi_complie _ _ADITIONAL_LIGHTS
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RealTimeLights.hlsl"
#endif
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
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
                float3 normalWS : NORMAL;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };
            
            #include "Assets/Shaders/CustomShaderConfigs.hlsl"

            sampler2D _MainTex;
            
            CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;
            half3 _Tint;
            float _Alpha;
            float _FadeStrength;
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

                o.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                


                return o;
            }


            // Calculo de incidencia da luz
            half3 CalculateLight(Light light, float3 normal)
            {
                half3 diffuse = saturate(dot(normal, light.direction) * SHADOW_HARDNESS);

                return diffuse * light.shadowAttenuation * light.color;
            }

            // Calculo de luz sem normal
            half3 CalculateSecondLight(Light light)
            {
                half3 diffuse = (light.distanceAttenuation * LIGHT_ATTENUATION);

                return diffuse * light.color;
            }

            half4 frag (v2f IN) : SV_Target
            {
                half4 col = 0;
                half3 lightVal = 0;

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
                col = tex2D(_MainTex, IN.uv);

                col.rgb = (col * lightVal) + (col * _Tint * (1-lightVal));
                
                clip(col.a-0.5);

                //col.rgb = col * _Tint;
                
                col.a = _Alpha - IN.vertex.z * _FadeStrength;
                

                return col;
            }

            

            ENDHLSL
        }

        // This pass is used when drawing to a _CameraNormalsTexture texture
        Pass
        {
            Name "DepthNormals"
            Tags
            {
                "LightMode" = "DepthNormals"
            }

            // -------------------------------------
            // Render State Commands
            ZWrite On
            Cull[_Cull]

            HLSLPROGRAM
            #pragma target 2.0

            // -------------------------------------
            // Shader Stages
            #pragma vertex DepthNormalsVertex
            #pragma fragment DepthNormalsFragment

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local _NORMALMAP
            #pragma shader_feature_local _PARALLAXMAP
            #pragma shader_feature_local _ _DETAIL_MULX2 _DETAIL_SCALED
            #pragma shader_feature_local _ALPHATEST_ON
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

            // -------------------------------------
            // Unity defined keywords
            #pragma multi_compile _ LOD_FADE_CROSSFADE

            // -------------------------------------
            // Universal Pipeline keywords
            #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RenderingLayers.hlsl"

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"

            // -------------------------------------
            // Includes
            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitDepthNormalsPass.hlsl"
            ENDHLSL
        }
    }
    Fallback "Diffuse"
}
