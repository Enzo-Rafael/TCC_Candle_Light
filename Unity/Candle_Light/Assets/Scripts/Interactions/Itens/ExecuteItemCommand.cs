/**************************************************************
    Jogos Digitais SG
    ExecuteItemCommand

    Descrição: Dita como o objeto ira reagir a interação com determinado item.

    Candle Light - Jogos Digitais LURDES –  13/03/2024
    Modificado por: Italo 
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using UnityEngine;
using UnityEditor;
public enum ItemType{Single, Multiple}

#if UNITY_EDITOR

[CustomEditor(typeof(ExecuteItemCommand))]
public class ExecuteItemCommand_Editor: Editor{

    public override void OnInspectorGUI(){

        serializedObject.Update();
        base.OnInspectorGUI();
        SerializedProperty itemTypeProp = serializedObject.FindProperty("_itemType");
        SerializedProperty multipleCodeProp = serializedObject.FindProperty("_multipleCode");
        if ((ItemType)itemTypeProp.enumValueIndex == ItemType.Multiple) EditorGUILayout.PropertyField(multipleCodeProp, new GUIContent("Código de Múltiplas Interações"));
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
public class ExecuteItemCommand : Interactable, IObserver
{
    [Tooltip("Tipo do item")]
    [SerializeField] 
    private ItemType _itemType;

    [Tooltip("Referência para codigo que terá como esse item funciona")]
    [HideInInspector]
    [SerializeField]
    protected MonoBehaviour _multipleCode;
    protected IMultiple _multiple => _multipleCode as IMultiple;
    

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
        RegisterEvent();
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
        Debug.Log("Chamei?");
        if(_multipleCode != null && !_multiple.Validator(additionalInformation)) return;
        ExecuteOrder(message, additionalInformation, _script);
        Debug.Log(_script);
        Debug.Log(_customScript);
    }
    /*------------------------------------------------------------------------------
    Função:     UnregisterEvent
    Descrição:  Desregistra o Objeto na lista de Observadores do item especifico.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/ 
    protected override bool UnregisterEvent(){
        UnregisterEventPublic();
        return true;
    }

    public void RegisterEvent(){
        _observerEvent.RegisterObserver(this);
    }
    public void UnregisterEventPublic(){
        _observerEvent.UnregisterObserver(this);
    }
}
