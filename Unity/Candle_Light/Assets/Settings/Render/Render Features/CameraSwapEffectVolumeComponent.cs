using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Componente de inspetor para a CameraSwapEffectRendererFeature
/// </summary>
[VolumeComponentMenu("Post-processing Custom/CamSwapEffect")]
[VolumeRequiresRendererFeatures(typeof(CameraSwapEffectRendererFeature))]
[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
public sealed class CameraSwapEffectVolumeComponent : VolumeComponent, IPostProcessComponent
{
    public CameraSwapEffectVolumeComponent()
    {
        displayName = "Cam Swap Effect";
    }

    [Tooltip("Forca do efeito.")]
    public ClampedFloatParameter intensity = new ClampedFloatParameter(1f, 0f, 1f);


    public bool IsActive()
    {
        return intensity.GetValue<float>() > 0.0f;
    }
}
