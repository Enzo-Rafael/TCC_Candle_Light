using UnityEngine;
using UnityEditor;


public enum ItemActionType{Toggle, Cosume, Trigger}

#if UNITY_EDITOR
[CustomEditor(typeof(Interactable))]
public class Interactable_Editor: Editor{

    public override void OnInspectorGUI(){

        serializedObject.Update();

        SerializedProperty itemTypeProp = serializedObject.FindProperty("_itemType");
        SerializedProperty multipleCodeProp = serializedObject.FindProperty("_multipleCode");
        SerializedProperty actionTypeProp = serializedObject.FindProperty("_actionType");

        // Mostra o tipo do item (Single / Multiple)
        EditorGUILayout.PropertyField(itemTypeProp);

        // Mostra o tipo de ação (Trigger, Toggle, Cosume)
        EditorGUILayout.PropertyField(actionTypeProp);

        // Mostra todas as outras propriedades, exceto as que já manipulamos
        DrawPropertiesExcluding(serializedObject, "_itemType", "_multipleCode", "_actionType");

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

    public virtual void ExecuteOrder(int message){
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
    }
    /*------------------------------------------------------------------------------
    Função:     UnregisterEvent
    Descrição:  Desregistra o Objeto na lista de Observadores do item especifico.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/ 
    protected virtual void UnregisterEvent(){}
}
