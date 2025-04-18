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

public enum ItemActionType{Toggle, Cosume, Trigger, Multiple}

[CustomEditor(typeof(ExecuteItemCommand))]
public class ExecuteCommand_Editor: Editor{

    public override void OnInspectorGUI(){

        serializedObject.Update();

        SerializedProperty actionTypeProp = serializedObject.FindProperty("_actionType");
        SerializedProperty multipleProp = serializedObject.FindProperty("multiple");

        EditorGUILayout.PropertyField(actionTypeProp);

        if ((ItemActionType)actionTypeProp.enumValueIndex == ItemActionType.Multiple){
           EditorGUILayout.PropertyField(multipleProp);
        }
        DrawPropertiesExcluding(serializedObject, "_actionType", "multiple");

        serializedObject.ApplyModifiedProperties();
    }
}

public class ExecuteItemCommand : MonoBehaviour, IObserver
{

    //-------------------------- Variaveis Globais Visiveis --------------------------------

    [HideInInspector]
    public int multiple;

    private int _message;

    [Tooltip("Referência para o evento sendo escutado.")]
	[SerializeField] 
    private ObserverEventChannel _observerEvent = default;

    [Tooltip("Referência para o controlador de animacao.")]
	[SerializeField] 
    private Animator animator;

    [Tooltip("Tipo de ação que o item fará ao interagirem com ele")]
    [SerializeField] 
    private ItemActionType _actionType;

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
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void OnEventRaised(int message){
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
            case ItemActionType.Multiple:
            _message += message > 0 ? 1 : -1;
            if(_message == multiple) animator.SetBool(parameterName, message != 0);
            break;
        }
    }
    
    private void UnregisterEvent(){
        _observerEvent.UnregisterObserver(this);
    }
}
