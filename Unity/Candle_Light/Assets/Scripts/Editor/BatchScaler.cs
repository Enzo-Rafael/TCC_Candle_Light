using UnityEngine;
using UnityEditor;

public class BatchScaler : EditorWindow
{
    public float factor;

    private SerializedObject soScaler;

    private SerializedProperty spFactor;

    [MenuItem("Tools/Alu/BatchScaler")]
    public static void OpenWindow()
    {
        EditorWindow window = GetWindow(typeof(BatchScaler));
        window.titleContent = new GUIContent("Batch Scaler");
    }

    private void OnEnable()
    {
        soScaler = new SerializedObject(this);
        spFactor = soScaler.FindProperty("factor");
    }

    private void OnGUI()
    {
        soScaler.Update();

        GUILayout.BeginHorizontal();
        spFactor.floatValue = EditorGUILayout.FloatField(spFactor.floatValue, GUILayout.Width(100));

        soScaler.ApplyModifiedProperties();

        if (GUILayout.Button("Scale Selected"))
        {
            Undo.SetCurrentGroupName("Scaled Game Objects");
            int group = Undo.GetCurrentGroup();

            for (int i = 0; i < Selection.gameObjects.Length; i++)
            {
                Undo.RecordObject(Selection.gameObjects[i].transform, "Transform_Scaled");

                Selection.gameObjects[i].transform.localScale *= factor;
            }

            Undo.CollapseUndoOperations(group);
        }
        GUILayout.EndHorizontal();
    }
}
