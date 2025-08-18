
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using CandleLight.Editor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

[CustomEditor(typeof(ExecuteItemCommand))]
[CanEditMultipleObjects]
public class ExecuteItemEditor : InteractableEditor
{
    protected override void AddInspectorProperties(VisualElement ux)
    {
        ux.AddHeader("Execute Item Command Configurações");

        var Target = target as ExecuteItemCommand;
        var targetGameObject = Target.gameObject;

        var multipleCodeProp = serializedObject.FindProperty("_multipleCode");
        ux.Add(new PropertyField(multipleCodeProp));
        List<Type> availableTypes = ComponentFinder.GetTypes(typeof(IMultiple));
        var choices = new List<string> { "Single" };
        choices.AddRange(availableTypes.Select(type => ObjectNames.NicifyVariableName(type.Name)));

        var dropdownRow = new EditorUIUtils.LabeledRow("Item Type", "Single: Executa sempre que um item interage com ele(caso ele não seja consume) \nMultiple: Tem que atender multiplas condiões para executar");
        ux.Add(dropdownRow);

        var multipleTypeDropdown = new DropdownField("", choices, 0);
        multipleTypeDropdown.style.flexGrow = 1; 
        
        dropdownRow.Contents.Add(multipleTypeDropdown);

        var currentComponent = multipleCodeProp.objectReferenceValue as MonoBehaviour;
        if (currentComponent != null){
            int previouslySelectedIndex = availableTypes.FindIndex(type => type == currentComponent.GetType());
            if (previouslySelectedIndex != -1){
                multipleTypeDropdown.index = previouslySelectedIndex + 1; 
            }
        }

        multipleTypeDropdown.RegisterValueChangedCallback(evt =>
        {
            var oldComponent = multipleCodeProp.objectReferenceValue as MonoBehaviour;
            int newIndex = multipleTypeDropdown.index;

            if (oldComponent != null){
                Undo.DestroyObjectImmediate(oldComponent);
            }
            if (newIndex > 0){
                Type newType = availableTypes[newIndex - 1];
                var newComponent = Undo.AddComponent(targetGameObject, newType);
                multipleCodeProp.objectReferenceValue = newComponent;
            }
            else{
                multipleCodeProp.objectReferenceValue = null;
            }
            Debug.Log(multipleCodeProp.objectReferenceValue);
            serializedObject.ApplyModifiedProperties();
        });

        EditorUIUtils.AddHeader(ux, "Dados de Estado e Jogo");
        ux.Add(new PropertyField(serializedObject.FindProperty("indexPuzzle")));
        ux.Add(new PropertyField(serializedObject.FindProperty("spawnProx")));
        ux.Add(new PropertyField(serializedObject.FindProperty("canSave")));

        var statusRow = new EditorUIUtils.LabeledRow("Status Atual do Puzzle");
        ux.Add(statusRow);
        
        serializedObject.Update();
        var statusLabel = new Label("Verificando..."); 
        statusRow.Contents.Add(statusLabel);
        ux.ContinuousUpdate(() =>{
                if (!Application.isPlaying)
                    statusRow.SetVisible(false);
                else{
                    statusRow.SetVisible(true);
                    bool isCompleted = Target.completed;
                    if (isCompleted){
                    statusLabel.text = "Concluído";
                    statusLabel.style.color = new StyleColor(new Color(0.2f, 0.8f, 0.2f));
                }
                else{
                    statusLabel.text = "Incompleto";
                    statusLabel.style.color = StyleKeyword.Null;
                }
                ;
            }
        });
        EditorUIUtils.AddSpace(ux);
        ux.Add(new PropertyField(serializedObject.FindProperty("interactions")));

    }
}
