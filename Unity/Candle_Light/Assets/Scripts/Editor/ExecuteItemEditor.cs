
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using CandleLight.Editor;
using UnityEngine;

[CustomEditor(typeof(ExecuteItemCommand))]
[CanEditMultipleObjects]
public class ExecuteItemEditor : InteractableEditor
{
    protected override void AddInspectorProperties(VisualElement ux)
    {
        ux.AddHeader("Execute Item Command Configurações");

        var Target = target as ExecuteItemCommand; 

        var itemTypeProp = serializedObject.FindProperty("_itemType");
        var multipleCodeProp = serializedObject.FindProperty("_multipleCode");
        
        var itemTypeField = new PropertyField(itemTypeProp);
        ux.Add(itemTypeField);


        var multipleCodeField = new PropertyField(multipleCodeProp, "Código de Múltiplas Interações");
        ux.Add(multipleCodeField);

        void UpdateItemCommandFields(ItemType currentType){
            EditorUIUtils.SetVisible(multipleCodeField, currentType == ItemType.Multiple);
        }

        UpdateItemCommandFields((ItemType)itemTypeProp.enumValueIndex);

        itemTypeField.RegisterValueChangeCallback(evt =>
        {
            UpdateItemCommandFields((ItemType)evt.changedProperty.enumValueIndex);
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
            if (Target != null){
                bool isCompleted = Target.completed;
                if (isCompleted){
                    statusLabel.text = "Concluído";
                    statusLabel.style.color = new StyleColor(new Color(0.2f, 0.8f, 0.2f));
                }
                else{
                    statusLabel.text = "Pendente";
                    statusLabel.style.color = StyleKeyword.Null;
                }
                ;
            }
        });
        EditorUIUtils.AddSpace(ux);
        ux.Add(new PropertyField(serializedObject.FindProperty("interactions")));

    }
}
