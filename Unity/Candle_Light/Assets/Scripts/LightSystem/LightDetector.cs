using System;
using UnityEngine;

/// <summary>
/// Detector de luz que sincroniza com o sistema de luz.
/// </summary>
/*
*   Registra um detector no sistema de luz no Awake e remove no OnDestroy
*/
public class LightDetector : MonoBehaviour
{
    /// <summary>
    /// Propriedade que retorna o status de iluminacao mais recente.
    /// </summary>
    public bool IsLit
    {
        get => _isLit;
    }

    private bool _isLit;

    /// <summary>
    /// Habilite para sincronizar a posicao no update de fisica.
    /// </summary>
    [Header("Habilite para sincronizar a posicao no update de fisica")]
    [SerializeField] private bool doPositionSync;

    public Action<bool> LightChangeEvent;

    void FixedUpdate()
    {
        // Sincroniza posicao com o sistema de luz
        if(doPositionSync)
        {
            LightSystem.Instance.UpdateDetectorPos(GetInstanceID(), transform.position);
        }
    }


    void OnEnable()
    {
        LightSystem.Instance.AddDetector(transform.position, (lit)=> {if(_isLit!=lit) _isLit = lit; LightChangeEvent(lit);}, GetInstanceID());
    }


    void OnDisable()
    {
        LightSystem.Instance.RemoveDetector(GetInstanceID());
    }
}
