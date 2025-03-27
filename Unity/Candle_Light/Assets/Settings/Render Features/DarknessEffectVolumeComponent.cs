using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Componente de inspetor para a DarknessEffectRendererFeature
/// </summary>
[VolumeComponentMenu("Post-processing Custom/DarknessEffect")]
[VolumeRequiresRendererFeatures(typeof(DarknessEffectRendererFeature))]
[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
public sealed class DarknessEffectVolumeComponent : VolumeComponent, IPostProcessComponent
{
    public DarknessEffectVolumeComponent()
    {
        displayName = "Darkness Effect";
    }

    [Tooltip("Forca do efeito.")]
    public ClampedFloatParameter intensity = new ClampedFloatParameter(1f, 0f, 1f);

    public bool IsActive()
    {
        return intensity.GetValue<float>() > 0.0f;
    }
}
