using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using CandleLight.Editor;
using System;

[CustomEditor(typeof(Interactable), true)]
[CanEditMultipleObjects]
public class InteractableEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        var ux = new VisualElement();
        ux.Add(new PropertyField(serializedObject.FindProperty("_observerEvent")));

        EditorUIUtils.AddHeader(ux, "Configurações de Animação");

        var actionTypeProp = serializedObject.FindProperty("_actionType");
        var actionTypeField = new PropertyField(actionTypeProp);
        ux.Add(actionTypeField);

        var animationSettingsContainer = new VisualElement();
        ux.Add(animationSettingsContainer);

        var animatorField = new PropertyField(serializedObject.FindProperty("animator"));
        var parameterNameField = new PropertyField(serializedObject.FindProperty("parameterName"));
        var invertParameterField = new PropertyField(serializedObject.FindProperty("_invertParameter"));

        animationSettingsContainer.Add(animatorField);
        animationSettingsContainer.Add(parameterNameField);
        animationSettingsContainer.Add(invertParameterField);

        void UpdateAnimationFieldsVisibility(ItemActionType currentType){
            EditorUIUtils.SetVisible(animationSettingsContainer, currentType != ItemActionType.None);
            EditorUIUtils.SetVisible(invertParameterField, currentType == ItemActionType.Toggle);
        }

        UpdateAnimationFieldsVisibility((ItemActionType)actionTypeProp.enumValueIndex);
        actionTypeField.RegisterValueChangeCallback(evt =>
        {
            UpdateAnimationFieldsVisibility((ItemActionType)evt.changedProperty.enumValueIndex);
        });

        EditorUIUtils.AddSpace(ux);

        AddInspectorProperties(ux);
        EditorUIUtils.AddHeader(ux, "Script Custom");

        var customScriptsList = new PropertyField(serializedObject.FindProperty("_customScripts"));
        ux.Add(customScriptsList);

        return ux;
    }
    
        protected virtual void AddInspectorProperties(VisualElement ux)
        {
            var p = serializedObject.FindProperty("_customScripts");
            if (p.NextVisible(false))
                EditorUIUtils.AddRemainingProperties(ux, p);
        }}