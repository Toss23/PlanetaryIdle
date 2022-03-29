using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Resources))]
public class ResourcesEditor : Editor
{
    // Style Configuration
    private Color defaultColor;

    // Properties
    private SerializedProperty prefabs;
    private int arraySize;
    private bool[] deleteElements;

    private void OnEnable()
    {
        defaultColor = GUI.backgroundColor;
        prefabs = serializedObject.FindProperty("prefabs");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Title
        GUILayout.BeginHorizontal();
        GUILayout.Label("Resources", TEditor.TitleStyle());
        GUILayout.EndHorizontal();
        TEditor.TitleSpace();

        // Array size and delete list
        arraySize = prefabs.arraySize;
        deleteElements = new bool[arraySize];

        // Show all resources
        for (int index = 0; index < arraySize; index++)
            ShowResource(index);

        // Delete resources
        for (int index = 0; index < deleteElements.Length; index++)
            if (deleteElements[index])
            {
                prefabs.DeleteArrayElementAtIndex(index);
                arraySize = prefabs.arraySize;
            }

        // Add resource
        ShowAddResource();

        serializedObject.ApplyModifiedProperties();
    }

    private void ShowResource(int index)
    {
        GUILayout.BeginVertical(GUI.skin.button);
        TEditor.Space();

        // Find prefab
        SerializedProperty prefab = prefabs.GetArrayElementAtIndex(index);
        SerializedProperty identifier = prefab.FindPropertyRelative("identifier");
        SerializedProperty haveMaximum = prefab.FindPropertyRelative("haveMaximum");
        SerializedProperty identifierMaximum = prefab.FindPropertyRelative("identifierMaximum");
        SerializedProperty maximum = prefab.FindPropertyRelative("maximum");
        bool haveMaximumOnLoad = haveMaximum.boolValue;

        // Title
        GUILayout.BeginHorizontal();
        TEditor.HorizontalBorder();
        GUILayout.Label(identifier.stringValue, TEditor.SubTitleStyle());

        // Move element up and down
        GUI.backgroundColor = Color.cyan;
        if (GUILayout.Button("Up", GUILayout.Width(TEditor.TextWidth)))
        {
            if (index != 0)
                prefabs.MoveArrayElement(index, index - 1);
        }
        if (GUILayout.Button("Down", GUILayout.Width(TEditor.TextWidth)))
        {
            if (index != arraySize - 1)
                prefabs.MoveArrayElement(index, index + 1);
        }
        GUI.backgroundColor = defaultColor;

        // Delete
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Delete", GUILayout.Width(TEditor.TextWidth)))
            deleteElements[index] = true;
        GUI.backgroundColor = defaultColor;

        GUILayout.EndHorizontal();
        TEditor.TitleSpace();

        // Edit identifier
        GUILayout.BeginHorizontal();
        TEditor.HorizontalBorder(2);
        GUILayout.Label("Identifier: ", GUILayout.Width(TEditor.TextWidth));
        identifier.stringValue = EditorGUILayout.TextField(identifier.stringValue, GUILayout.Width(TEditor.FieldWidth));

        // Toggle Have maximum
        TEditor.HorizontalBorder(2);
        haveMaximum.boolValue = EditorGUILayout.Toggle(haveMaximum.boolValue, GUILayout.Width(TEditor.ToggleWidth));
        GUILayout.Label("Have maximum", GUILayout.Width(TEditor.TextWidth));
        GUILayout.EndHorizontal();

        TEditor.Space();

        // Maximum
        if (haveMaximum.boolValue)
        {
            if (haveMaximumOnLoad != haveMaximum.boolValue)
                identifierMaximum.stringValue = identifier.stringValue + "Maximum";

            // Title
            GUILayout.BeginHorizontal();
            TEditor.HorizontalBorder(2);
            GUILayout.Label("Maximum");
            GUILayout.EndHorizontal();

            // Identifier
            GUILayout.BeginHorizontal();
            TEditor.HorizontalBorder(2);
            TEditor.Tab();
            GUILayout.Label("Identifier: ", GUILayout.Width(TEditor.TextWidth - TEditor.border - TEditor.tab));
            identifierMaximum.stringValue = EditorGUILayout.TextField(identifierMaximum.stringValue, GUILayout.Width(TEditor.FieldWidth));
            GUILayout.EndHorizontal();

            // Value
            GUILayout.BeginHorizontal();
            TEditor.HorizontalBorder(2);
            TEditor.Tab();
            GUILayout.Label("Start value: ", GUILayout.Width(TEditor.TextWidth - TEditor.border - TEditor.tab));
            maximum.intValue = EditorGUILayout.IntField(maximum.intValue, GUILayout.Width(TEditor.FieldWidth));
            GUILayout.EndHorizontal();
            TEditor.Space();
        }

        TEditor.Space();
        GUILayout.EndVertical();
        TEditor.Space();
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

            // Defualt Values
            identifier.stringValue = "Resource";
            haveMaximum.boolValue = false;
            identifierMaximum.stringValue = "ResourceMaximum";
            maximum.intValue = 0;
        }

        GUILayout.EndVertical();
    }
}