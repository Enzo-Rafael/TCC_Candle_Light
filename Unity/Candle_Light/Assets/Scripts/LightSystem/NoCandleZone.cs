using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Rendering;

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

    private bool _disabled;

    void Awake()
    {
        _disabled = false;
        lightsContained = new List<PointLight>();
        col = GetComponent<BoxCollider>();
    }


    void FixedUpdate()
    {
        if (_disabled) return;

        foreach (PointLight light in LightSystem.Instance.pointLights)
        {
            if (col.bounds.Contains(light.transform.position))
            {
                lightsContained.Add(light);
                light.visualLight.enabled = false;
                light.enabled = false;
                LightSystem.Instance.RemovePointLight(light);
                return;
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
                return;
            }
        }
    }

    /// <summary>
    /// Inicia o processo de desligamento desse efeito. Luzes sao reativadas uma a uma em uma cadencia e ordem aleatoria.
    /// </summary>
    public void Disperse()
    {
        _disabled = true;
        StartCoroutine(DisperseCoroutine());
    }

    private IEnumerator DisperseCoroutine()
    {
        while (lightsContained.Count > 0)
        {
            yield return new WaitForSeconds(Random.Range(0.2f, 1f));

            int randIndex = Random.Range(0, lightsContained.Count);

            LightSystem.Instance.AddPointLight(lightsContained[randIndex]);
            lightsContained[randIndex].visualLight.enabled = true;
            lightsContained[randIndex].enabled = true;
            lightsContained.RemoveAt(randIndex);
        }
    }
    #endregion
}
