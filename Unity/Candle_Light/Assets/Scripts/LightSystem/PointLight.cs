using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Detector de luz que sincroniza com o sistema de luz.
/// </summary>
/*
*   Registra uma luz no sistema de luz no Awake e remove no OnDestroy
*/
[RequireComponent(typeof(Light))]
public class PointLight : MonoBehaviour
{
    public float radius;

    public Light visualLight;

    public GameObject litSprite;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
        if(visualLight)
            visualLight.range = radius;

    }

    void OnEnable()
    {
        LightSystem.Instance.AddPointLight(this);
        if (litSprite != null) litSprite.SetActive(true);
        visualLight.enabled = true;
    }

    void OnDisable()
    {
        LightSystem.Instance.RemovePointLight(this);
        if (litSprite != null) litSprite.SetActive(false);
        visualLight.enabled = false;
    }

}
