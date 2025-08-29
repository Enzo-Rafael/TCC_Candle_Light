using UnityEngine;
using System.Collections.Generic; 

public enum ItemActionType{None, Toggle, Consume, Trigger}

public class Interactable : MonoBehaviour
{
 //-------------------------- Variaveis Globais Visiveis --------------------------------

    [Tooltip("Tipo de ação que o item fará ao interagirem com ele")]
    [SerializeField]
    protected ItemActionType _actionType;
    
    [Tooltip("Referência para o evento sendo escutado.")]
    [SerializeField]
    protected List<ObserverEventChannel> _observerEvent = default;

    [Tooltip("Se marcado, permite configurar uma animação para a ação selecionada.")]
    [SerializeField]
    private bool _useAnimation;
    
    [Tooltip("Referência para o controlador de animacao.")]
    [SerializeField]
    protected Animator animator;

    [Tooltip("Nome do parametro de animador a ser modificado.")]
    [SerializeField]
    protected string parameterName;

    [SerializeField]
    protected bool _invertParameter;


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
    protected void ExecuteOrder(int message = 1)
    {
        if (consumeBool) return;
        switch (_actionType)
        {
            case ItemActionType.Trigger:
                if (animator != null) animator.SetTrigger(parameterName);
                break;
            case ItemActionType.Toggle:
                if (animator != null) animator.SetBool(parameterName, message != 0);
                CustomScript((message != 0) != _invertParameter);
                return;
            case ItemActionType.Consume:
                if (animator != null) animator.SetTrigger(parameterName);
                UnregisterEvent();
                consumeBool = true;
                break;
        }
        CustomScript();
    }

    private void CustomScript(object additionalInformation = null){
        if (_customScripts != null){
            Debug.Log("Custom Script Check");
            foreach (var scriptComponent in _customScripts){
                if (scriptComponent is ICodeCustom script){
                    Debug.Log("Custom Script Activated");
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
    protected virtual void UnregisterEvent() { }

}
