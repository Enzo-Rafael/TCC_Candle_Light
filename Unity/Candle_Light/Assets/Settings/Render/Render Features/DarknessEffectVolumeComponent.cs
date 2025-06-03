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

    [Tooltip("Distancia maxima de visao.")]
    public MinFloatParameter distance = new MinFloatParameter(10, 0, true);
    [Tooltip("Efeito de feedback para quando o fantasma se mostra.")]
    public MinFloatParameter showFeedback = new MinFloatParameter(1, 0, true);

    public bool IsActive()
    {
        return intensity.GetValue<float>() > 0.0f;
    }
}
