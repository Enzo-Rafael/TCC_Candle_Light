/**************************************************************
    Jogos Digitais SG
    ExecuteItemCommand

    Descrição: Dita como o objeto ira reagir a interação com determinado item.

    Candle Light - Jogos Digitais LURDES –  13/03/2024
    Modificado por: Italo 
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public enum ItemType{Single, Multiple}
public enum ItemActionType{Toggle, Cosume, Trigger}

#if UNITY_EDITOR
[CustomEditor(typeof(ExecuteItemCommand))]
public class ExecuteCommand_Editor: Editor{

    public override void OnInspectorGUI(){

        serializedObject.Update();

        SerializedProperty itemTypeProp = serializedObject.FindProperty("_itemType");
        SerializedProperty multipleCodeProp = serializedObject.FindProperty("_multipleCode");
        SerializedProperty actionTypeProp = serializedObject.FindProperty("_actionType");

        // Mostra o tipo do item (Single / Multiple)
        EditorGUILayout.PropertyField(itemTypeProp);

        // Se for do tipo Multiple, mostra o campo do script
        if ((ItemType)itemTypeProp.enumValueIndex == ItemType.Multiple)
        {
            EditorGUILayout.PropertyField(multipleCodeProp, new GUIContent("Código de Múltiplas Interações"));
        }

        // Mostra o tipo de ação (Trigger, Toggle, Cosume)
        EditorGUILayout.PropertyField(actionTypeProp);

        // Mostra todas as outras propriedades, exceto as que já manipulamos
        DrawPropertiesExcluding(serializedObject, "_itemType", "_multipleCode", "_actionType");

        serializedObject.ApplyModifiedProperties();
    }
}
#endif

public class ExecuteItemCommand : MonoBehaviour, IObserver
{

    //-------------------------- Variaveis Globais Visiveis --------------------------------
    
    [Tooltip("Referência para codigo que terá como esse item funciona")]
    [HideInInspector]
    [SerializeField]
    private MonoBehaviour _multipleCode;
    private IMultiple _multiple => _multipleCode as IMultiple;

    [Tooltip("Referência para o evento sendo escutado.")]
	[SerializeField] 
    private ObserverEventChannel _observerEvent = default;

    [Tooltip("Referência para o controlador de animacao.")]
	[SerializeField] 
    private Animator animator;

    [Tooltip("Tipo de ação que o item fará ao interagirem com ele")]
    [SerializeField] 
    private ItemActionType _actionType;

    [Tooltip("Tipo do item")]
    [SerializeField] 
    private ItemType _itemType;

    [Tooltip("Nome do parametro de animador a ser modificado.")]
    [SerializeField]
    private string parameterName;


    //Pega referência do animation
    private void Start(){
       if(animator == null) animator = GetComponentInParent<Animator>();
    }
    /*------------------------------------------------------------------------------
    Função:     OnEnable
    Descrição:  Registra o Objeto na lista de Observadores do item especifico.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void OnEnable(){
        _observerEvent.RegisterObserver(this);
    }
    /*------------------------------------------------------------------------------
    Função:     OnDisable
    Descrição:  Desregistra o Objeto na lista de Observadores do item especifico.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void OnDisable(){
        UnregisterEvent();
    }
    /*------------------------------------------------------------------------------
    Função:     OnEventRaised
    Descrição:  Chama a função respectiva do Atuador, para que ele possa executar sua função.
    Entrada:    int - indentificação para dizer qual ação o atuador fará.
                object - Informação com tipo generico do que o objeto faz
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void OnEventRaised(int message, object additionalInformation){
        
        if(_multipleCode != null && !_multiple.Validator(additionalInformation)) return;
        switch(_actionType){
            case ItemActionType.Trigger:
                animator.SetTrigger(parameterName);
            break;

            case ItemActionType.Toggle:
                animator.SetBool(parameterName, message != 0);
            break;

            case ItemActionType.Cosume:
            animator.SetTrigger(parameterName);
            UnregisterEvent();
            break;
        }
    }
    /*------------------------------------------------------------------------------
    Função:     UnregisterEvent
    Descrição:  Desregistra o Objeto na lista de Observadores do item especifico.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/ 
    private void UnregisterEvent(){
        _observerEvent.UnregisterObserver(this);
    }
}
