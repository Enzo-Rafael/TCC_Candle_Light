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
    [SerializeField] private float radius;

    [SerializeField] private Light light;

    /// <summary>
    /// Habilite para sincronizar a posicao no update de fisica.
    /// </summary>
    //[Header("Habilite para sincronizar a posicao no update de fisica")]
    //[SerializeField] private bool doPositionSync;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
        if(light)
            light.range = radius;
        
    }

    void FixedUpdate()
    {
        // Sincroniza posicao com o sistema de luz
        //if (doPositionSync)
        //{
            LightSystem.Instance.UpdatePointLightPos(GetInstanceID(), transform.position);
        //}
    }


    void OnEnable()
    {
        LightSystem.Instance.AddPointLight(transform.position, radius, GetInstanceID());
    }

    void OnDisable()
    {
        LightSystem.Instance.RemovePointLight(GetInstanceID());
    }

}
