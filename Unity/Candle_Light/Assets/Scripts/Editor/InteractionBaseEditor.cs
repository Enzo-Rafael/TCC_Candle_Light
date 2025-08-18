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
        EditorUIUtils.AddHeader(parent, "Configurações de Animação");

        var actionTypeField = new PropertyField(_actionTypeProp);
        parent.Add(actionTypeField);

        var container = new VisualElement();
        parent.Add(container);
        
        var invertField = new PropertyField(_invertParameterProp);
        container.Add(new PropertyField(_animatorProp));
        container.Add(new PropertyField(_parameterNameProp));
        container.Add(invertField);

        void UpdateVisibility(ItemActionType type) {
            EditorUIUtils.SetVisible(container, type != ItemActionType.None);
            EditorUIUtils.SetVisible(invertField, type == ItemActionType.Toggle);
        }

        actionTypeField.RegisterValueChangeCallback(evt => UpdateVisibility((ItemActionType)evt.changedProperty.enumValueIndex));
        UpdateVisibility((ItemActionType)_actionTypeProp.enumValueIndex);
    }

    private void BuildCustomScriptSection(VisualElement parent) {
        var targetInteractable = target as Interactable;
        var targetGameObject = targetInteractable.gameObject;
        EditorUIUtils.AddSpace(parent);
        EditorUIUtils.AddHeader(parent, "Scripts Custom");

        var addActionRow = new EditorUIUtils.LabeledRow("Add Script Custom", "Clique para adicionar um novo componente de script customizado.");
        parent.AddChild(addActionRow);

        var addButton = new Button { text = "(select)" };
        addButton.style.flexGrow = 1; 
        addButton.style.unityTextAlign = TextAnchor.MiddleLeft;
        addActionRow.Contents.Add(addButton);

        List<Type> validTypes = ComponentFinder.GetTypes(typeof(ICodeCustom));
        var contextMenu = new ContextualMenuManipulator(evt => {
            foreach (var scriptType in validTypes) {
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
            var savedList = new List<MonoBehaviour>();
            for (int i = 0; i < _customScriptsProp.arraySize; i++) {
                var item = _customScriptsProp.GetArrayElementAtIndex(i).objectReferenceValue as MonoBehaviour;
                if (item != null) savedList.Add(item);
            }

            if (!actualComponents.SequenceEqual(savedList)) {
                _customScriptsProp.ClearArray();
                for (int i = 0; i < actualComponents.Count; i++) {
                    _customScriptsProp.InsertArrayElementAtIndex(i);
                    _customScriptsProp.GetArrayElementAtIndex(i).objectReferenceValue = actualComponents[i];
                }
                serializedObject.ApplyModifiedProperties();
            }
        });
    }

    protected virtual void AddInspectorProperties(VisualElement ux) {
        var p = serializedObject.FindProperty("_customScripts");
        if (p.NextVisible(false)) {
            EditorUIUtils.AddRemainingProperties(ux, p);
        }
    }
}