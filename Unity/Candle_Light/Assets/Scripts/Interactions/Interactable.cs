using UnityEngine;
using UnityEditor;


public enum ItemActionType{Toggle, Cosume, Trigger}

#if UNITY_EDITOR
[CustomEditor(typeof(Interactable))]
public class Interactable_Editor: Editor{

    public override void OnInspectorGUI(){

        serializedObject.Update();

        SerializedProperty actionTypeProp = serializedObject.FindProperty("_actionType");
        SerializedProperty enableScriptProp = serializedObject.FindProperty("_enableCustomScript");
        SerializedProperty customScriptProp = serializedObject.FindProperty("_customScript");

        EditorGUILayout.PropertyField(actionTypeProp);
        EditorGUILayout.PropertyField(enableScriptProp);

        if (enableScriptProp.boolValue)EditorGUILayout.PropertyField(customScriptProp, new GUIContent("Script Customizado"));
        
        serializedObject.ApplyModifiedProperties();
    }
}
#endif

public class Interactable : MonoBehaviour
{
 //-------------------------- Variaveis Globais Visiveis --------------------------------

    [Tooltip("Referência para o evento sendo escutado.")]
	[SerializeField] 
    protected ObserverEventChannel _observerEvent = default;

    [Tooltip("Referência para o controlador de animacao.")]
	[SerializeField] 
    protected Animator animator;

    [Tooltip("Tipo de ação que o item fará ao interagirem com ele")]
    [SerializeField] 
    protected ItemActionType _actionType;

    [Tooltip("Nome do parametro de animador a ser modificado.")]
    [SerializeField]
    protected string parameterName;


    [Tooltip("Habilta o script custom.")]
    [SerializeField] private bool _enableCustomScript;

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
    protected virtual void ExecuteOrder(int message){
            switch(_actionType){
            case ItemActionType.Trigger:
                animator.SetTrigger(parameterName);
            break;

            case ItemActionType.Toggle:
                animator.SetBool(parameterName, message != 0);
            break;

            case ItemActionType.Cosume:
            UnregisterEvent();
            animator.SetTrigger(parameterName);
            break;
        }
        if(_script != null) _script.CustomBaseAction();
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
