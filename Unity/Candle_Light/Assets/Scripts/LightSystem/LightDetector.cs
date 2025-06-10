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

    public Action<bool> lightChangeEvent;

    [Tooltip("Referência para os objetos que receberão os comandos da interação")]
	[SerializeField]
    private ObserverEventChannel _observerEvent = default;

    [Tooltip("Referência para o animator. DEVE TER UM PARAMETRO BOOL CHAMADO \"IsLit\".")]
    [SerializeField]
    private Animator animator;

    void Awake()
    {
        lightChangeEvent = (x) => { };
        if (_observerEvent)
            lightChangeEvent += (x) => _observerEvent.NotifyObservers(x ? 1 : 0);
        if (animator != null)
            lightChangeEvent += (x) => animator.SetBool("IsLit", x);
    }

    void FixedUpdate()
    {

        LightSystem.Instance.UpdateDetectorPos(GetInstanceID(), transform.position);

    }


    void OnEnable()
    {
        LightSystem.Instance.AddDetector(
                transform.position,
                (lit)=>
                {
                    if(_isLit != lit)
                    {
                        _isLit = lit;
                        lightChangeEvent(lit);
                    };
                },
                GetInstanceID());
    }


    void OnDisable()
    {
        LightSystem.Instance.RemoveDetector(GetInstanceID());
    }
}
