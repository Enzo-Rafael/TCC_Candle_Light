using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.RenderGraphModule.Util;

public class DarknessEffectRendererFeature : ScriptableRendererFeature
{
    class DarknessEffectPass : ScriptableRenderPass
    {
        const string m_PassName = "DarknessEffectPass";
        Material m_BlitMaterial;

        // Propriedades de shader
        private static readonly int intensityID = Shader.PropertyToID("_Intensity");
        private static readonly int distanceID = Shader.PropertyToID("_MaxDistance");
        private static readonly int showID = Shader.PropertyToID("_ShowFeedback");

        public void Setup(Material mat)
        {
            m_BlitMaterial = mat;
            requiresIntermediateTexture = true;
        }

        // RecordRenderGraph is where the RenderGraph handle can be accessed, through which render passes can be added to the graph.
        // FrameData is a context container through which URP resources can be accessed and managed.
        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            var stack = VolumeManager.instance.stack;
            var customEffect = stack.GetComponent<DarknessEffectVolumeComponent>();

            if(!customEffect.IsActive())
                return;

            m_BlitMaterial.SetFloat(intensityID, (float)customEffect.intensity);
            m_BlitMaterial.SetFloat(distanceID, (float)customEffect.distance);
            m_BlitMaterial.SetFloat(showID, (float)customEffect.showFeedback);

            var resourceData = frameData.Get<UniversalResourceData>();

            if(resourceData.isActiveTargetBackBuffer)
            {
                Debug.LogError($"Pulando render pass. DarknessEffectRenderFeature por algum motivo tentou usar o BackBuffer como input.");
                return;
            }

            var source = resourceData.activeColorTexture;

            var destinationDesc = renderGraph.GetTextureDesc(source);
            destinationDesc.name = $"CameraColor-{m_PassName}";
            destinationDesc.clearBuffer = false;

            TextureHandle destination = renderGraph.CreateTexture(destinationDesc);

            RenderGraphUtils.BlitMaterialParameters para = new(source, destination, m_BlitMaterial, 0);
            renderGraph.AddBlitPass(para, passName: m_PassName);

            resourceData.cameraColor = destination;
        }
    }

    DarknessEffectPass m_ScriptablePass;

    // Propriedades
    public RenderPassEvent injectionPoint = RenderPassEvent.BeforeRenderingPostProcessing;
    public Material material;

    /// <inheritdoc/>
    public override void Create()
    {
        m_ScriptablePass = new DarknessEffectPass();

        // Configures where the render pass should be injected.
        m_ScriptablePass.renderPassEvent = injectionPoint;
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if(material == null)
        {
            Debug.LogWarning("DarknessEffectRenderFeature sem material e sera ignorada.");
            return;
        }

        m_ScriptablePass.Setup(material);
        renderer.EnqueuePass(m_ScriptablePass);
    }
}
