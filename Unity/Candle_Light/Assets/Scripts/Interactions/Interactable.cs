using UnityEngine;
using System.Collections.Generic; 

public enum ItemActionType{None, Toggle, Cosume, Trigger}

public class Interactable : MonoBehaviour
{
 //-------------------------- Variaveis Globais Visiveis --------------------------------

    [Tooltip("Tipo de ação que o item fará ao interagirem com ele")]
    [SerializeField]
    protected ItemActionType _actionType;

    [Tooltip("Habilita o uso de um Observer Event Channel para este objeto.")]
    [SerializeField]
    protected bool _useObserverEvent; 
    
    [Tooltip("Referência para o evento sendo escutado.")]
    [SerializeField]
    protected ObserverEventChannel _observerEvent = default;


    [Tooltip("Referência para o controlador de animacao.")]
    [SerializeField]
    protected Animator animator;

    [Tooltip("Nome do parametro de animador a ser modificado.")]
    [SerializeField]
    protected string parameterName;

    [SerializeField]
    protected bool _invertParameter;

    [Tooltip("Habilita a lista de scripts customizados.")]
    [SerializeField] public bool _useCustomScripts;


    [Tooltip("Lista de scripts customizados que serão executados quando interagir com o item")]
    [SerializeField]
    protected List<MonoBehaviour> _customScripts;

    protected bool consumeBool = false;

    /*------------------------------------------------------------------------------
    Função:     ExecuteOrder
    Descrição:  Executa a animação do item.
    Entrada:    int - indentificação para dizer qual ação o atuador fará.
    Saída:      -
    ------------------------------------------------------------------------------*/
    protected virtual void ExecuteOrder(int message = 1, object additionalInformation = null)
    {
        switch (_actionType)
        {
            case ItemActionType.Trigger:
                if (animator != null) animator.SetTrigger(parameterName);
                break;

            case ItemActionType.Toggle:
                if (animator != null) animator.SetBool(parameterName, message != 0);
                additionalInformation = (message != 0) != _invertParameter;
                break;

            case ItemActionType.Cosume:
                if (!consumeBool && animator != null) animator.SetTrigger(parameterName);
                UnregisterEvent();
                consumeBool = true;
                break;
        }
        if (_useCustomScripts && _customScripts != null){
            foreach (var scriptComponent in _customScripts){
                if (scriptComponent is ICodeCustom script){
                    script.CustomBaseAction(additionalInformation);
                }
            }
        }

    }
    /*------------------------------------------------------------------------------
    Função:     UnregisterEvent
    Descrição:  Desregistra o Objeto na lista de Observadores do item especifico.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/ 
    protected virtual void UnregisterEvent(){}

    protected ObserverEventChannel GetObserver(){
        return _observerEvent;
    }
    protected void SetObserver(ObserverEventChannel observerEvent){
        _observerEvent = observerEvent;
    }
}
