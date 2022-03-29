using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Miner))]
public class MinerEditor : Editor
{
    // Properties
    private SerializedProperty resourceIdentifier;
    private SerializedProperty level;
    private SerializedProperty configuration;

    // Resources Identifiers
    private string[] resourceIdentifiers;

    private void OnEnable()
    {
        // Properties
        resourceIdentifier = serializedObject.FindProperty("resourceIdentifier");
        level = serializedObject.FindProperty("level");
        configuration = serializedObject.FindProperty("configuration");

        // Resource Identifiers
        resourceIdentifiers = TEditor.ResourceIdentifiers();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Title
        GUILayout.BeginHorizontal();
        GUILayout.Label("Miner", TEditor.TitleStyle());
        GUILayout.EndHorizontal();
        TEditor.TitleSpace();

        // Resource
        if (resourceIdentifiers.Length != 0)
        {
            int index = TEditor.GetResourceIndex(resourceIdentifier.stringValue, resourceIdentifiers);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Resource: ", GUILayout.Width(TEditor.TextWidth));
            index = EditorGUILayout.Popup(index, resourceIdentifiers, GUILayout.Width(TEditor.FieldWidth));
            resourceIdentifier.stringValue = resourceIdentifiers[index];
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Level: ", GUILayout.Width(TEditor.TextWidth));
            level.intValue = EditorGUILayout.IntField(level.intValue, GUILayout.Width(TEditor.FieldWidth));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Configuration: ", GUILayout.Width(TEditor.TextWidth));
            configuration.objectReferenceValue = EditorGUILayout.ObjectField(configuration.objectReferenceValue, typeof(Configuration), true, GUILayout.Width(TEditor.FieldWidth));
            GUILayout.EndHorizontal();
        }
        else
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Please create Resources System component and mark by Tag");
            GUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
    }
}