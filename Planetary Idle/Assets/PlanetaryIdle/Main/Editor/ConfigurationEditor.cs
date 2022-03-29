using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Configuration))]
public class ConfigurationEditor : Editor
{
    // Style Configuration
    private Color defaultColor;
    private GUIStyle mainTitle;
    private GUIStyle subTitle;

    private int space = 5;
    private int border = 5;

    private int textWidth = 70;
    private int fieldWidth = 100;
    private int toggleWidth = 15;
    private int tab = 5;

    // Properties
    private int levelsCount;
    private SerializedProperty levels;
    private SerializedProperty haveInput;

    // Resources System
    private ResourcesSystem resourcesSystem;
    private string[] resourceIdentifiers;

    private void OnEnable()
    {
        // Style Configuration
        defaultColor = GUI.backgroundColor;

        mainTitle = new GUIStyle();
        mainTitle.fontStyle = FontStyle.BoldAndItalic;
        mainTitle.normal.textColor = Color.white;
        mainTitle.fontSize = 18;

        subTitle = new GUIStyle();
        subTitle.fontStyle = FontStyle.BoldAndItalic;
        subTitle.normal.textColor = Color.white;
        subTitle.fontSize = 16;

        // Properties
        levels = serializedObject.FindProperty("levels");
        levelsCount = levels.arraySize;
        haveInput = serializedObject.FindProperty("haveInput");

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

        // Levels
        if (levelsCount == 0 || levels == null)
        {
            levelsCount = 1;
            levels.arraySize = levelsCount;
        }

        if (resourcesSystem != null)
        {
            for (int level = 0; level < levelsCount; level++)
            {
                SerializedProperty serializedProperty = levels.GetArrayElementAtIndex(level);
                SerializedProperty priceResource = serializedProperty.FindPropertyRelative("priceResource");
                SerializedProperty price = serializedProperty.FindPropertyRelative("price");

                GUILayout.BeginVertical(GUI.skin.button);
                GUILayout.Space(border);
                if (level == 0)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("", GUILayout.Width(border));
                    GUILayout.Label("Build price", subTitle);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("", GUILayout.Width(border));
                    GUILayout.Label("Value: ", GUILayout.Width(textWidth));
                    price.intValue = EditorGUILayout.IntField(price.intValue, GUILayout.Width(fieldWidth));
                    GUILayout.Label("", GUILayout.Width(tab));
                    GUILayout.Label("Resource: ", GUILayout.Width(textWidth));
                    priceResource.stringValue = EditorGUILayout.TextField(priceResource.stringValue, GUILayout.Width(fieldWidth));
                    GUILayout.EndHorizontal();
                }
                else
                {

                }
                GUILayout.Space(border);
                GUILayout.EndVertical();
                GUILayout.Space(space);
            }
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