using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

//================
// TODO: Fazer o VolumeManipulator ser um componente separado e generico
//================

/// <summary>
/// O BoxCollider trigger associado com esse objeto desligara luzes que estiverem dentro dele, e as ligara de novo quando sairem.
/// </summary>
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Volume))]
public class NoCandleZone : MonoBehaviour
{
    #region NO_CANDLE_ZONE
    private List<PointLight> lightsContained;

    private BoxCollider col;

    void Awake()
    {
        lightsContained = new List<PointLight>();
        col = GetComponent<BoxCollider>();

        // coisa do manipulador de volume
        volumeProfile = GetComponent<Volume>().profile;
        volumeProfile.TryGet(out filmGrainComponent);
        volumeProfile.TryGet(out colorCurvesComponent);
    }

    void FixedUpdate()
    {
        foreach (PointLight light in LightSystem.Instance.pointLights)
        {
            if (col.bounds.Contains(light.transform.position))
            {
                lightsContained.Add(light);
                light.visualLight.enabled = false;
                light.enabled = false;
                LightSystem.Instance.RemovePointLight(light);
            }
        }

        foreach (PointLight light in lightsContained)
        {
            if (!col.bounds.Contains(light.transform.position))
            {
                LightSystem.Instance.AddPointLight(light);
                light.visualLight.enabled = true;
                light.enabled = true;
                lightsContained.Remove(light);
            }
        }
    }

    /// <summary>
    /// Inicia o processo de desligamento desse efeito. Luzes sao reativadas uma a uma em uma cadencia e ordem aleatoria.
    /// </summary>
    public void Disperse()
    {
        StartCoroutine(DisperseCoroutine());
    }

    private IEnumerator DisperseCoroutine()
    {
        while (lightsContained.Count > 0)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));

            int randIndex = Random.Range(0, lightsContained.Count);

            LightSystem.Instance.AddPointLight(lightsContained[randIndex]);
            lightsContained[randIndex].visualLight.enabled = true;
            lightsContained[randIndex].enabled = true;
            lightsContained.RemoveAt(randIndex);
        }
    }
    #endregion

    #region VOLUME_MANIPULATOR
    private VolumeProfile volumeProfile;

    [SerializeField]
    private float grainIntensity;

    [SerializeField]
    private float colorCurveIntensity;
    const float curveKFStart = 0.1f, curveKFEnd = 0.5f;

    private FilmGrain filmGrainComponent;
    private ColorCurves colorCurvesComponent;


    void OnAnimatorMove()
    {
        filmGrainComponent.intensity.Override(grainIntensity);
        colorCurvesComponent.lumVsSat.value.MoveKey(0, new Keyframe(0, colorCurveIntensity * 0.5f));
    }

    void OnDrawGizmosSelected()
    {
        filmGrainComponent.intensity.Override(grainIntensity);
        colorCurvesComponent.lumVsSat.value.MoveKey(0, new Keyframe(0, Mathf.Lerp(curveKFEnd, curveKFStart, colorCurveIntensity)));
    }

    #endregion
}
