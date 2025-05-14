using UnityEngine;

public enum ItemActionType{Toggle, Cosume, Trigger}

public class Interactable : MonoBehaviour
{
 //-------------------------- Variaveis Globais Visiveis --------------------------------

    [Tooltip("Tipo de ação que o item fará ao interagirem com ele")]
    [SerializeField] 
    protected ItemActionType _actionType;

    [Tooltip("Referência para o evento sendo escutado.")]
	[SerializeField] 
    protected ObserverEventChannel _observerEvent = default;

    [Tooltip("Referência para o controlador de animacao.")]
	[SerializeField] 
    protected Animator animator;


    [Tooltip("Nome do parametro de animador a ser modificado.")]
    [SerializeField]
    protected string parameterName;


    [Tooltip("Habilta o script custom.")]
    [HideInInspector]
    [SerializeField] public bool _enableCustomScript;

    [Tooltip("Referência para script custom que será executado quando iteragir com o item")]
    [SerializeField]
    protected MonoBehaviour _customScript;
    protected ICustomCode _script => _customScript as ICustomCode;

    /*------------------------------------------------------------------------------
    Função:     ExecuteOrder
    Descrição:  Executa a animação do item.
    Entrada:    int - indentificação para dizer qual ação o atuador fará.
    Saída:      -
    ------------------------------------------------------------------------------*/
    protected virtual void ExecuteOrder(int message = 1, object additionalInformation = null, ICustomCode script = null){
            switch(_actionType){
            case ItemActionType.Trigger:
                animator.SetTrigger(parameterName);
            break;

            case ItemActionType.Toggle:
                animator.SetBool(parameterName, message != 0);
            break;

            case ItemActionType.Cosume:
            if(UnregisterEvent()) return;
            animator.SetTrigger(parameterName);
            break;
        }
        if(_script != null) _script.CustomBaseAction(additionalInformation);
    }
    /*------------------------------------------------------------------------------
    Função:     UnregisterEvent
    Descrição:  Desregistra o Objeto na lista de Observadores do item especifico.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/ 
    protected virtual bool UnregisterEvent(){
        return true;
    }

    protected ObserverEventChannel GetObserver(){
        return _observerEvent;
    }
    protected void SetObserver(ObserverEventChannel observerEvent){
        _observerEvent = observerEvent;
    }
}
