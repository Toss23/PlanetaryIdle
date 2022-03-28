using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

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

    // Resources System
    private ResourcesSystem resourcesSystem;
    private string[] resourceIdentifiers;

    private void OnEnable()
    {
        // Style Configuration
        mainTitle = new GUIStyle();
        mainTitle.fontStyle = FontStyle.BoldAndItalic;
        mainTitle.normal.textColor = Color.white;
        mainTitle.fontSize = 18;

        // Properties
        resourceIdentifier = serializedObject.FindProperty("resourceIdentifier");
        level = serializedObject.FindProperty("level");
        configuration = serializedObject.FindProperty("configuration");

        // Build Resource Identifiers Array
        BuildIdentifiersArray();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Title
        GUILayout.BeginHorizontal();
        GUILayout.Label("Miner", mainTitle);
        GUILayout.EndHorizontal();
        GUILayout.Space(space * 2);

        // Resource
        int index = 0;
        for (int i = 0; i < resourceIdentifiers.Length; i++)
        {
            if (resourceIdentifiers[i] == resourceIdentifier.stringValue)
            {
                index = i;
                break;
            }
        }

        if (resourcesSystem != null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Resource: ", GUILayout.Width(textWidth));
            index = EditorGUILayout.Popup(index, resourceIdentifiers, GUILayout.Width(fieldWidth));
            resourceIdentifier.stringValue = resourceIdentifiers[index];
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Level: ", GUILayout.Width(textWidth));
            level.intValue = EditorGUILayout.IntField(level.intValue, GUILayout.Width(fieldWidth));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Configuration: ", GUILayout.Width(textWidth));
            configuration.objectReferenceValue = EditorGUILayout.ObjectField(configuration.objectReferenceValue, typeof(Configuration), true, GUILayout.Width(fieldWidth));
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

    private void BuildIdentifiersArray()
    {
        GameObject gameObject = GameObject.FindWithTag("ResourcesSystem");
        if (gameObject != null)
        {
            resourcesSystem = gameObject.GetComponent<ResourcesSystem>();
            ResourcePrefab[] prefabs = resourcesSystem.Resources.Prefabs;
            List<string> identifiers = new List<string>();

            // Build List
            for (int index = 0; index < prefabs.Length; index++)
            {
                identifiers.Add(prefabs[index].Identifier);
                if (prefabs[index].HaveMaximum)
                    identifiers.Add(prefabs[index].IdentifierMaximum);
            }

            // Convert List to Array
            resourceIdentifiers = new string[identifiers.Count];
            for (int index = 0; index < resourceIdentifiers.Length; index++)
            {
                resourceIdentifiers[index] = identifiers[index];
            }
        }
    }
}