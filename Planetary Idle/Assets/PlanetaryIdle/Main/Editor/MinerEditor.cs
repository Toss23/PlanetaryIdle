using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Miner))]
public class MinerEditor : Editor
{
    // Properties
    private SerializedProperty level;
    private SerializedProperty configuration;

    private void OnEnable()
    {
        // Properties
        level = serializedObject.FindProperty("level");
        configuration = serializedObject.FindProperty("configuration");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Title
        GUILayout.BeginHorizontal();
        GUILayout.Label("Miner", TEditor.TitleStyle());
        GUILayout.EndHorizontal();
        TEditor.TitleSpace();

        // Properties
        if (configuration.objectReferenceValue != null)
        {
            
        }

        GUILayout.BeginHorizontal();
        GUILayout.Label("Level: ", GUILayout.Width(TEditor.TextWidth));
        level.intValue = EditorGUILayout.IntField(level.intValue, GUILayout.Width(TEditor.FieldWidth));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Configuration: ", GUILayout.Width(TEditor.TextWidth));
        configuration.objectReferenceValue = EditorGUILayout.ObjectField(configuration.objectReferenceValue, typeof(Configuration), true, GUILayout.Width(TEditor.FieldWidth));
        GUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }
}