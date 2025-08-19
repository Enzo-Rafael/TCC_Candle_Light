using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using CandleLight.Editor; 
using System.Collections.Generic;
using System.Linq;
using System;

[CustomEditor(typeof(Interactable), true)]
[CanEditMultipleObjects]
public class InteractableEditor : Editor {
    
    private SerializedProperty _observerEventProp;
    private SerializedProperty _actionTypeProp;
    private SerializedProperty _animatorProp;
    private SerializedProperty _parameterNameProp;
    private SerializedProperty _invertParameterProp;
    private SerializedProperty _customScriptsProp;

    public override VisualElement CreateInspectorGUI() {
        var ux = new VisualElement();
        
        FindAllProperties();

        ux.Add(new PropertyField(_observerEventProp));
        BuildAnimationSettings(ux);
        AddInspectorProperties(ux);
        BuildCustomScriptSection(ux);

        return ux;
    }

    private void FindAllProperties() {
        _observerEventProp = serializedObject.FindProperty("_observerEvent");
        _actionTypeProp = serializedObject.FindProperty("_actionType");
        _animatorProp = serializedObject.FindProperty("animator");
        _parameterNameProp = serializedObject.FindProperty("parameterName");
        _invertParameterProp = serializedObject.FindProperty("_invertParameter");
        _customScriptsProp = serializedObject.FindProperty("_customScripts");
    }

    private void BuildAnimationSettings(VisualElement parent) {
        EditorUIUtils.AddSpace(parent);
        EditorUIUtils.AddHeader(parent, "Configurações do Interagivel");
        var actionTypeField = parent.AddChild(new PropertyField(_actionTypeProp));

        var useAnimationProp = serializedObject.FindProperty("_useAnimation");
        var useAnimationToggle = new PropertyField(useAnimationProp, "Use Animation");
        parent.AddChild(useAnimationToggle);
        
        var animationFieldsContainer = parent.AddChild(new VisualElement());
        
        var invertField = new PropertyField(_invertParameterProp);
        animationFieldsContainer.AddChild(new PropertyField(_animatorProp));
        animationFieldsContainer.AddChild(new PropertyField(_parameterNameProp));
        animationFieldsContainer.AddChild(invertField);

        void UpdateVisibility(){
            var currentType = (ItemActionType)_actionTypeProp.enumValueIndex;
            var useAnimation = useAnimationProp.boolValue;

            EditorUIUtils.SetVisible(useAnimationToggle, currentType != ItemActionType.None);

            bool showAnimationFields = currentType != ItemActionType.None && useAnimation;
            EditorUIUtils.SetVisible(animationFieldsContainer, showAnimationFields);
            
            EditorUIUtils.SetVisible(invertField, showAnimationFields && currentType == ItemActionType.Toggle);
        }
        var Target = target as Interactable;
        actionTypeField.RegisterValueChangeCallback(evt => UpdateVisibility());
        useAnimationToggle.RegisterValueChangeCallback(evt => {
            UpdateVisibility();
            if (evt.changedProperty.boolValue == true) {
            if (_animatorProp.objectReferenceValue == null) {
                Animator foundAnimator = Target.GetComponent<Animator>() ?? Target.GetComponentInParent<Animator>();
                if (foundAnimator != null) {
                    _animatorProp.objectReferenceValue = foundAnimator;
                }
            }
        }else{
            _animatorProp.objectReferenceValue = null;
        }
        serializedObject.ApplyModifiedProperties();
        });
        UpdateVisibility();
    }

    private void BuildCustomScriptSection(VisualElement parent) {
        var targetInteractable = target as Interactable;
        var targetGameObject = targetInteractable.gameObject;
        EditorUIUtils.AddSpace(parent);
        EditorUIUtils.AddHeader(parent, "Scripts Custom");
        var addActionRow = new EditorUIUtils.LabeledRow("Add Script Custom", "Clique para adicionar um novo componente de script customizado.");
        parent.AddChild(addActionRow);
        var addButton = new Button { text = "(Select)" };
        addButton.style.flexGrow = 1; 
        addButton.style.unityTextAlign = TextAnchor.MiddleLeft;
        addActionRow.Contents.Add(addButton);

        List<Type> validTypes = ComponentFinder.GetTypes(typeof(ICodeCustom));
        var contextMenu = new ContextualMenuManipulator(evt => {
            foreach (var scriptType in validTypes){
                evt.menu.AppendAction(
                    ObjectNames.NicifyVariableName(scriptType.Name),
                    action => {
                        var newComponent = Undo.AddComponent(targetGameObject, scriptType);
                        _customScriptsProp.InsertArrayElementAtIndex(_customScriptsProp.arraySize);
                        _customScriptsProp.GetArrayElementAtIndex(_customScriptsProp.arraySize - 1).objectReferenceValue = newComponent;
                        serializedObject.ApplyModifiedProperties();
                    },
                    action => DropdownMenuAction.Status.Normal
                );
            }
        });
        contextMenu.activators.Clear();
        contextMenu.activators.Add(new ManipulatorActivationFilter { button = MouseButton.LeftMouse });
        addButton.AddManipulator(contextMenu);

    parent.ContinuousUpdate(() => {
        if (targetInteractable == null) return;
        
        var actualComponents = targetInteractable.GetComponents<MonoBehaviour>().Where(s => s is ICodeCustom).ToList();
        bool needsResync = false;
        if (actualComponents.Count != _customScriptsProp.arraySize){
            needsResync = true;
        }else{
            for (int i = 0; i < actualComponents.Count; i++){
                var savedRef = _customScriptsProp.GetArrayElementAtIndex(i).objectReferenceValue;
                if (savedRef != actualComponents[i]){
                    needsResync = true;
                    break;
                }
            }
        }
        if (needsResync){
            _customScriptsProp.ClearArray();
            for (int i = 0; i < actualComponents.Count; i++){
                _customScriptsProp.InsertArrayElementAtIndex(i);
                _customScriptsProp.GetArrayElementAtIndex(i).objectReferenceValue = actualComponents[i];
            }
            serializedObject.ApplyModifiedProperties();
        }
    });
    }

    protected virtual void AddInspectorProperties(VisualElement ux){
        var p = serializedObject.FindProperty("_customScripts");
        if (p.NextVisible(false)){
            EditorUIUtils.AddRemainingProperties(ux, p);
        }
    }
}