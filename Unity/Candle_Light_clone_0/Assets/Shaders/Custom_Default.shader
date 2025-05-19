Shader "Custom/Custom_Default"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ShadowTint ("Shadow Tint", Color) = (0.5,0.5,0.5)
        [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull", Float) = 0
        [MaterialToggle] _Highlight ("Highlight", Float) = 0
    }
    SubShader
    {
        Pass
        {
            Name "ForwardPass"
            ZWrite ON
            Cull[_Cull]

            Tags {
                "Queue"="Geometry"
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
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };
            
            #include "Assets/Shaders/CustomShaderConfigs.hlsl"

            sampler2D _MainTex;
            half _Highlight;
            
            CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;
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
                half timer = abs(frac(_Time.z)*2-1);

                if(_Highlight == 1)
                {
                    col.rgb += saturate(1-dot(IN.normalWS, inputData.viewDirectionWS)) * HIGHLIGHT_COLOR * (timer/2+0.5);
                }

                col.rgb = (col * lightVal) + (col * _ShadowTint * (1-lightVal));
                
                clip(col.a-0.5);
                return col;
            }

            

            ENDHLSL
        }


        // ShadowCaster
        Pass
        {
            Name "ShadowCaster"
            Tags
            {
                "LightMode" = "ShadowCaster"
            }

            // -------------------------------------
            // Render State Commands
            ZWrite On
            ZTest LEqual
            ColorMask 0
            Cull Off

            HLSLPROGRAM
            #pragma target 2.0

            // -------------------------------------
            // Shader Stages
            #pragma vertex ShadowPassVertex
            #pragma fragment ShadowPassFragment

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local _ALPHATEST_ON
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"

            // -------------------------------------
            // Universal Pipeline keywords

            // -------------------------------------
            // Unity defined keywords
            #pragma multi_compile _ LOD_FADE_CROSSFADE

            // This is used during shadow map generation to differentiate between directional and punctual light shadows, as they use different formulas to apply Normal Bias
            #pragma multi_compile_vertex _ _CASTING_PUNCTUAL_LIGHT_SHADOW

            // -------------------------------------
            // Includes
            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/ShadowCasterPass.hlsl"
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

            sampler2D _MainTex;
            
            float4 _MainTex_ST;

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
            #include "Packages/com.unity.render-pipelines.universal/Shaders/UnlitInput.hlsl"

            #ifndef UNIVERSAL_UNLIT_DEPTH_NORMALS_PASS_INCLUDED
            #define UNIVERSAL_UNLIT_DEPTH_NORMALS_PASS_INCLUDED

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #if defined(LOD_FADE_CROSSFADE)
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
            #endif

            struct Attributes
            {
                float3 normal       : NORMAL;
                float4 positionOS   : POSITION;
                float4 tangentOS    : TANGENT;
                float2 texcoord     : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionCS   : SV_POSITION;
                #if defined(_ALPHATEST_ON)
                    float2 uv       : TEXCOORD0;
                #endif
                float3 normalWS     : TEXCOORD1;
            
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

            Varyings DepthNormalsVertex(Attributes input)
            {
                Varyings output = (Varyings)0;
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_TRANSFER_INSTANCE_ID(input, output);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
            
                #if defined(_ALPHATEST_ON)
                    output.uv = TRANSFORM_TEX(input.texcoord, _BaseMap);
                #endif
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
            
                VertexNormalInputs normalInput = GetVertexNormalInputs(input.normal, input.tangentOS);
                output.normalWS = NormalizeNormalPerVertex(normalInput.normalWS);
            
                return output;
            }

            void DepthNormalsFragment(
                Varyings input
                , out half4 outNormalWS : SV_Target0
            #ifdef _WRITE_RENDERING_LAYERS
                , out float4 outRenderingLayers : SV_Target1
            #endif
            )
            {
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
            
                #if defined(_ALPHATEST_ON)
                    Alpha(SampleAlbedoAlpha(input.uv, TEXTURE2D_ARGS(_BaseMap, sampler_BaseMap)).a, _BaseColor, _Cutoff);
                #endif
            
                #if defined(LOD_FADE_CROSSFADE)
                    LODFadeCrossFade(input.positionCS);
                #endif
            
                // Output...
                #if defined(_GBUFFER_NORMALS_OCT)
                    float3 normalWS = normalize(input.normalWS);
                    float2 octNormalWS = PackNormalOctQuadEncode(normalWS);             // values between [-1, +1], must use fp32 on some platforms
                    float2 remappedOctNormalWS = saturate(octNormalWS * 0.5 + 0.5);     // values between [ 0,  1]
                    half3 packedNormalWS = half3(PackFloat2To888(remappedOctNormalWS)); // values between [ 0,  1]
                    outNormalWS = half4(packedNormalWS, 0.0);
                #else
                    outNormalWS = half4(NormalizeNormalPerPixel(input.normalWS), 0.0);
                #endif
            
                #ifdef _WRITE_RENDERING_LAYERS
                    uint renderingLayers = GetMeshRenderingLayer();
                    outRenderingLayers = float4(EncodeMeshRenderingLayer(renderingLayers), 0, 0, 0);
                #endif
            }

            #endif

            ENDHLSL
        }
    }
    Fallback "Diffuse"
}
