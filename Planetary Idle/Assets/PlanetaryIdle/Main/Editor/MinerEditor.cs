using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Miner))]
public class MinerEditor : Editor
{
    // Style Configuration
    private GUIStyle mainTitle;

    private int space = 5;

    private int textWidth = 120;
    private int fieldWidth = 200;

    // Properties
    private SerializedProperty resourceIdentifier;
    private SerializedProperty level;
    private SerializedProperty configuration;

    private void OnEnable()
    {
        mainTitle = new GUIStyle();
        mainTitle.fontStyle = FontStyle.BoldAndItalic;
        mainTitle.normal.textColor = Color.white;
        mainTitle.fontSize = 18;

        // Properties
        resourceIdentifier = serializedObject.FindProperty("resourceIdentifier");
        level = serializedObject.FindProperty("level");
        configuration = serializedObject.FindProperty("configuration");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Title
        GUILayout.BeginHorizontal();
        GUILayout.Label("Miner", mainTitle);
        GUILayout.EndHorizontal();
        GUILayout.Space(space * 2);

        /* Resource identifier
        GUILayout.BeginHorizontal();
        int index = 0;
        for (int i = 0; i < ResourcesEditor.ResourceIdentifiers.Length; i++)
        {
            if (ResourcesEditor.ResourceIdentifiers[i] == resourceIdentifier.stringValue)
                index = i;
        }
        index = EditorGUILayout.Popup(index, ResourcesEditor.ResourceIdentifiers);
        resourceIdentifier.stringValue = ResourcesEditor.ResourceIdentifiers[index];
        GUILayout.EndHorizontal();
        */

        serializedObject.ApplyModifiedProperties();
    }
}