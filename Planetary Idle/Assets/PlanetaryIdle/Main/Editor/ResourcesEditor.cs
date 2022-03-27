using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Resources))]
public class ResourcesEditor : Editor
{
    public static string[] ResourceIdentifiers;
    private static List<string> resourceIdentifiersList;

    // Style Configuration
    private Color defaultColor;
    private GUIStyle mainTitle;
    private GUIStyle subTitle;

    private int space = 5;
    private int border = 5;

    private int textWidth = 120;
    private int fieldWidth = 200;
    private int toggleWidth = 15;
    private int tab = 5;

    // Properties
    private SerializedProperty prefabs;
    private int arraySize;
    private bool[] deleteElements;
    private bool push;

    private void OnEnable()
    {
        defaultColor = GUI.backgroundColor;

        mainTitle = new GUIStyle();
        mainTitle.fontStyle = FontStyle.BoldAndItalic;
        mainTitle.normal.textColor = Color.white;
        mainTitle.fontSize = 18;

        subTitle = new GUIStyle();
        subTitle.fontStyle = FontStyle.BoldAndItalic;
        subTitle.normal.textColor = Color.white;
        subTitle.fontSize = 16;

        prefabs = serializedObject.FindProperty("prefabs");
        push = false;

        if (resourceIdentifiersList == null) resourceIdentifiersList = new List<string>();
        if (ResourceIdentifiers == null) ResourceIdentifiers = new string[0];
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Title
        GUILayout.BeginHorizontal();
        GUILayout.Label("Resources", mainTitle);
        GUI.backgroundColor = Color.cyan;
        if (GUILayout.Button("Push to List", GUILayout.MaxWidth(textWidth)))
        {
            push = true;
        }
        GUI.backgroundColor = defaultColor;
        GUILayout.EndHorizontal();
        GUILayout.Space(space * 2);

        // Array size and delete list
        arraySize = prefabs.arraySize;
        deleteElements = new bool[arraySize];

        // Show all resources
        for (int index = 0; index < arraySize; index++)
            ShowResource(index);

        // Add resource
        ShowAddResource();

        // Delete resources
        for (int index = 0; index < deleteElements.Length; index++)
            if (deleteElements[index])
                prefabs.DeleteArrayElementAtIndex(index);

        // Push resource identifiers to Array
        if (push)
        {
            // Build List
            resourceIdentifiersList.Clear();
            for (int index = 0; index < arraySize; index++)
                AddResourceIdentifierToList(index);

            // Convert List to Array
            ResourceIdentifiers = new string[resourceIdentifiersList.Count];
            for (int index = 0; index < resourceIdentifiersList.Count; index++)
                ResourceIdentifiers[index] = resourceIdentifiersList[index];

            push = false;
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void ShowResource(int index)
    {
        GUILayout.BeginVertical(GUI.skin.button);
        GUILayout.Space(border);

        // Find prefab
        SerializedProperty prefab = prefabs.GetArrayElementAtIndex(index);
        SerializedProperty identifier = prefab.FindPropertyRelative("identifier");
        SerializedProperty haveMaximum = prefab.FindPropertyRelative("haveMaximum");
        SerializedProperty identifierMaximum = prefab.FindPropertyRelative("identifierMaximum");
        SerializedProperty maximum = prefab.FindPropertyRelative("maximum");
        SerializedProperty dataKey = prefab.FindPropertyRelative("dataKey");
        bool haveMaximumOnLoad = haveMaximum.boolValue;
        string identifierOnLoad = identifier.stringValue;

        // Title
        GUILayout.BeginHorizontal();
        GUILayout.Label("", GUILayout.Width(border));
        GUILayout.Label(identifier.stringValue, subTitle);

        // Move element up and down
        GUI.backgroundColor = Color.cyan;
        if (GUILayout.Button("Up", GUILayout.Width(textWidth / 2)))
        {
            if (index != 0)
                prefabs.MoveArrayElement(index, index - 1);
        }
        if (GUILayout.Button("Down", GUILayout.Width(textWidth / 2)))
        {
            if (index != arraySize - 1)
                prefabs.MoveArrayElement(index, index + 1);
        }
        GUI.backgroundColor = defaultColor;

        // Delete
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Delete", GUILayout.Width(textWidth)))
            deleteElements[index] = true;
        GUI.backgroundColor = defaultColor;

        GUILayout.EndHorizontal();
        GUILayout.Space(border * 2);

        // Edit identifier
        GUILayout.BeginHorizontal();
        GUILayout.Label("", GUILayout.Width(border * 2));
        GUILayout.Label("Identifier: ", GUILayout.Width(textWidth));
        identifier.stringValue = EditorGUILayout.TextField(identifier.stringValue, GUILayout.Width(fieldWidth));

        // Toggle Have maximum
        GUILayout.Label("", GUILayout.Width(border));
        haveMaximum.boolValue = EditorGUILayout.Toggle(haveMaximum.boolValue, GUILayout.Width(toggleWidth));
        GUILayout.Label("Have maximum", GUILayout.Width(textWidth));
        GUILayout.EndHorizontal();

        GUILayout.Space(space);

        // Maximum
        if (haveMaximum.boolValue)
        {
            if (haveMaximumOnLoad != haveMaximum.boolValue)
                identifierMaximum.stringValue = identifier.stringValue + "Maximum";

            // Title
            GUILayout.BeginHorizontal();
            GUILayout.Label("", GUILayout.Width(border * 2));
            GUILayout.Label("Maximum");
            GUILayout.EndHorizontal();

            // Identifier
            GUILayout.BeginHorizontal();
            GUILayout.Label("", GUILayout.Width(border * 3 + tab));
            GUILayout.Label("Identifier: ", GUILayout.Width(textWidth - border - tab));
            identifierMaximum.stringValue = EditorGUILayout.TextField(identifierMaximum.stringValue, GUILayout.Width(fieldWidth));
            GUILayout.EndHorizontal();

            // Value
            GUILayout.BeginHorizontal();
            GUILayout.Label("", GUILayout.Width(border * 3 + tab));
            GUILayout.Label("Start value: ", GUILayout.Width(textWidth - border - tab));
            maximum.intValue = EditorGUILayout.IntField(maximum.intValue, GUILayout.Width(fieldWidth));
            GUILayout.EndHorizontal();

            GUILayout.Space(space);
        }

        // Data key
        bool overrideDataKey = false;
        bool overrideDataKeyOnLoad;

        if (identifierOnLoad == identifier.stringValue && identifier.stringValue != dataKey.stringValue)
            overrideDataKey = true;

        overrideDataKeyOnLoad = overrideDataKey;

        GUILayout.BeginHorizontal();
        GUILayout.Label("", GUILayout.Width(border * 2));
        GUILayout.Label("Override Data Key", GUILayout.Width(textWidth));
        overrideDataKey = EditorGUILayout.Toggle(overrideDataKey, GUILayout.Width(toggleWidth));
        GUILayout.EndHorizontal();

        if (overrideDataKey)
        {
            if (overrideDataKeyOnLoad != overrideDataKey && overrideDataKey == true)
                dataKey.stringValue = "";

            GUILayout.BeginHorizontal();
            GUILayout.Label("", GUILayout.Width(border * 3 + tab));
            GUILayout.Label("Data Key: ", GUILayout.Width(textWidth - border - tab));
            dataKey.stringValue = EditorGUILayout.TextField(dataKey.stringValue, GUILayout.Width(fieldWidth));
            GUILayout.EndHorizontal();
        }
        else
        {
            dataKey.stringValue = identifier.stringValue;
        }

        GUILayout.Space(border);
        GUILayout.EndVertical();
        GUILayout.Space(space);
    }

    private void ShowAddResource()
    {
        GUILayout.BeginVertical();

        GUIStyle buttonText = new GUIStyle(GUI.skin.button);
        buttonText.fontStyle = FontStyle.BoldAndItalic;
        buttonText.normal.textColor = Color.white;
        buttonText.fontSize = 14;

        if (GUILayout.Button("Add Resource", buttonText, GUILayout.Height(80)))
        {
            prefabs.InsertArrayElementAtIndex(arraySize);
            SerializedProperty prefab = prefabs.GetArrayElementAtIndex(arraySize);
            SerializedProperty identifier = prefab.FindPropertyRelative("identifier");
            SerializedProperty haveMaximum = prefab.FindPropertyRelative("haveMaximum");
            SerializedProperty identifierMaximum = prefab.FindPropertyRelative("identifierMaximum");
            SerializedProperty maximum = prefab.FindPropertyRelative("maximum");
            SerializedProperty dataKey = prefab.FindPropertyRelative("dataKey");

            // Defualt Values
            identifier.stringValue = "Resource";
            haveMaximum.boolValue = false;
            identifierMaximum.stringValue = "ResourceMaximum";
            maximum.intValue = 0;
            dataKey.stringValue = identifier.stringValue;
        }

        GUILayout.EndVertical();
    }

    private void AddResourceIdentifierToList(int index)
    {
        SerializedProperty prefab = prefabs.GetArrayElementAtIndex(index);
        SerializedProperty identifier = prefab.FindPropertyRelative("identifier");
        SerializedProperty haveMaximum = prefab.FindPropertyRelative("haveMaximum");
        SerializedProperty identifierMaximum = prefab.FindPropertyRelative("identifierMaximum");

        resourceIdentifiersList.Add(identifier.stringValue);
        if (haveMaximum.boolValue)
            resourceIdentifiersList.Add(identifierMaximum.stringValue);
    }
}