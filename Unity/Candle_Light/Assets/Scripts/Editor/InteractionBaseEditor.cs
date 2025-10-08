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
    
    private SerializedProperty _observerEventListenProp;
        private SerializedProperty _observerEventSpeakProp;     
    private SerializedProperty _actionTypeProp;
    private SerializedProperty _animatorProp;
    private SerializedProperty _parameterNameProp;
    private SerializedProperty _invertParameterProp;
    private SerializedProperty _customScriptsProp;

    public override VisualElement CreateInspectorGUI() {
        var ux = new VisualElement();
        
        FindAllProperties();
        ObserverSpeakList(ux);
        ux.Add(new PropertyField(_observerEventSpeakProp));
        BuildAnimationSettings(ux);
        AddInspectorProperties(ux);
        BaseActionExecute(ux);
        BuildCustomScriptSection(ux);

        return ux;
    }

    private void FindAllProperties() {
        _observerEventListenProp = serializedObject.FindProperty("_observerEventListening");
        _observerEventSpeakProp = serializedObject.FindProperty("_observerEventSpeak");
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
        parent.Add(useAnimationToggle);
        
        var animationFieldsContainer = parent.AddChild(new VisualElement());
        
        var invertField = new PropertyField(_invertParameterProp);
        animationFieldsContainer.Add(new PropertyField(_animatorProp));
        animationFieldsContainer.Add(new PropertyField(_parameterNameProp));
        animationFieldsContainer.Add(invertField);

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
    private void ObserverSpeakList(VisualElement parent){
        if(target is IObserver){
            parent.Add(new PropertyField(_observerEventListenProp));
        }
    }
    private void BaseActionExecute(VisualElement parent)
    {
        if (target is IInteractable targetAsInteractable)
        {
            EditorUIUtils.AddSpace(parent);
            var addActionRow = new EditorUIUtils.LabeledRow("Test Interactable");
            parent.Add(addActionRow);
            var useButton = new Button(() =>
            {
                targetAsInteractable?.BaseAction();
            });
            useButton.style.flexGrow = 1;
            useButton.style.unityTextAlign = TextAnchor.MiddleLeft;
            useButton.style.unityFontStyleAndWeight = FontStyle.Bold;
            useButton.style.paddingTop = 6;
            useButton.style.paddingBottom = 6;
            useButton.style.marginTop = 2;
            useButton.style.marginBottom = 2;

            addActionRow.Contents.Add(useButton);
            useButton.text = "Use Interactable";

            parent.ContinuousUpdate(() =>
            {
                if (!Application.isPlaying)
                {
                    addActionRow.SetVisible(false);
                }
                else
                {
                    addActionRow.SetVisible(true);
                }
            });
        }
    }

    private void BuildCustomScriptSection(VisualElement parent){
        EditorUIUtils.AddSpace(parent);
        EditorUIUtils.AddHeader(parent, "Scripts Custom");
        parent.Add(new PropertyField(_customScriptsProp));
    }

    protected virtual void AddInspectorProperties(VisualElement ux){
        var p = serializedObject.FindProperty("_customScripts");
        if (p.NextVisible(false)){
            EditorUIUtils.AddRemainingProperties(ux, p);
        }
    }
}