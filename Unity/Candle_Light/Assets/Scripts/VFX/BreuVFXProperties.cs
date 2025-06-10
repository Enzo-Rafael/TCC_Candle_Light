using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class BreuVFXProperties : MonoBehaviour
{
    private VolumeProfile volumeProfile;

    [SerializeField]
    private float grainIntensity;

    [SerializeField]
    private float colorCurveIntensity;
    const float curveKFStart = 0.1f, curveKFEnd = 0.5f;

    private FilmGrain filmGrainComponent;
    private ColorCurves colorCurvesComponent;

    void Awake()
    {
        volumeProfile = GetComponent<Volume>().profile;
        volumeProfile.TryGet(out filmGrainComponent);
        volumeProfile.TryGet(out colorCurvesComponent);
    }

    void OnAnimatorMove()
    {
        filmGrainComponent.intensity.Override(grainIntensity);
        colorCurvesComponent.lumVsSat.value.MoveKey(0, new Keyframe(0, colorCurveIntensity * 0.5f));
    }
}
