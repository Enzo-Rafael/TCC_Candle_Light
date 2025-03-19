using UnityEditor;
using UnityEngine;

/// <summary>
/// Detector de luz que sincroniza com o sistema de luz.
/// </summary>
/*
*   Registra uma luz no sistema de luz no Awake e remove no OnDestroy
*   Habilita/desabilita do sistema no enable/disable do componente
*/
public class PointLight : MonoBehaviour
{
    [SerializeField] private float radius;

    /// <summary>
    /// Habilite para sincronizar a posicao no update de fisica.
    /// </summary>
    [Header("Habilite para sincronizar a posicao no update de fisica")]
    [SerializeField] private bool doPositionSync;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void FixedUpdate()
    {
        // Sincroniza posicao com o sistema de luz
        if(doPositionSync)
        {
            LightSystem.Instance.UpdatePointLightPos(GetInstanceID(), transform.position);
        }
    }

    void Start()
    {
        LightSystem.Instance.AddPointLight(transform.position, radius, GetInstanceID());
    }

    void OnDisable()
    {
        LightSystem.Instance.PointLightSetActive(GetInstanceID(), false);
    }

    void OnEnable()
    {
        LightSystem.Instance.PointLightSetActive(GetInstanceID(), true);
    }

    void OnDestroy()
    {
        LightSystem.Instance.RemovePointLight(GetInstanceID());
    }
}
