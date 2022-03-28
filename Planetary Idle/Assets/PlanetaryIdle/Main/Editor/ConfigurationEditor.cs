using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Configuration))]
public class ConfigurationEditor : Editor
{
    // Style Configuration
    private GUIStyle mainTitle;
    private int space = 5;
    private int textWidth = 120;
    private int fieldWidth = 200;

    // Properties
    private SerializedProperty priceRersource;
    private SerializedProperty prices;

    private SerializedProperty haveConsumption;
    private SerializedProperty resourceInput;
    private SerializedProperty consumptions;

    private SerializedProperty productionInterval;
    private SerializedProperty resourceOutput;
    private SerializedProperty productions;

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


        // Build Resource Identifiers Array
        BuildIdentifiersArray();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Title
        GUILayout.BeginHorizontal();
        GUILayout.Label("Configuration", mainTitle);
        GUILayout.EndHorizontal();
        GUILayout.Space(space * 2);

        // 

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
