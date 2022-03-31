using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Configuration))]
public class ConfigurationEditor : Editor
{
    // Style Configuration
    private Color defaultColor;

    // Properties
    private int levelsCount;
    private SerializedProperty levels;
    private SerializedProperty haveInput;
    private SerializedProperty priceResourceSame;
    private SerializedProperty inputResourceSame;
    private SerializedProperty outputResourceSame;

    // For same resources
    private string priceResource;
    private string inputResource;
    private string outputResource;

    // Resources System
    private string[] resourceIdentifiers;
    private bool[] deleteElements;

    private void OnEnable()
    {
        // Style Configuration
        defaultColor = GUI.backgroundColor;

        // Properties
        levels = serializedObject.FindProperty("levels");
        haveInput = serializedObject.FindProperty("haveInput");
        priceResourceSame = serializedObject.FindProperty("priceResourceSame");
        inputResourceSame = serializedObject.FindProperty("inputResourceSame");
        outputResourceSame = serializedObject.FindProperty("outputResourceSame");

        // Build Resource Identifiers Array
        resourceIdentifiers = TEditor.ResourceIdentifiers();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Title
        GUILayout.BeginHorizontal();
        GUILayout.Label("Configuration (" + serializedObject.targetObject.name + ")", TEditor.TitleStyle());
        GUILayout.EndHorizontal();
        TEditor.TitleSpace();

        // Have Input
        GUILayout.BeginHorizontal();
        GUILayout.Label("Have Input Resource: ", GUILayout.Width(150));
        haveInput.boolValue = EditorGUILayout.Toggle(haveInput.boolValue, GUILayout.Width(TEditor.ToggleWidth));
        GUILayout.EndHorizontal();

        // Price Resource Same
        GUILayout.BeginHorizontal();
        GUILayout.Label("Price Resource Same: ", GUILayout.Width(150));
        priceResourceSame.boolValue = EditorGUILayout.Toggle(priceResourceSame.boolValue, GUILayout.Width(TEditor.ToggleWidth));
        GUILayout.EndHorizontal();

        // Input Resource Same
        GUILayout.BeginHorizontal();
        GUILayout.Label("Input Resource Same: ", GUILayout.Width(150));
        inputResourceSame.boolValue = EditorGUILayout.Toggle(inputResourceSame.boolValue, GUILayout.Width(TEditor.ToggleWidth));
        GUILayout.EndHorizontal();

        // Output Resource Same
        GUILayout.BeginHorizontal();
        GUILayout.Label("Output Resource Same: ", GUILayout.Width(150));
        outputResourceSame.boolValue = EditorGUILayout.Toggle(outputResourceSame.boolValue, GUILayout.Width(TEditor.ToggleWidth));
        GUILayout.EndHorizontal();

        TEditor.Space();

        // Levels
        levelsCount = levels.arraySize;
        if (levelsCount < 2 || levels == null)
        {
            levelsCount = 2;
            levels.arraySize = levelsCount;
        }

        deleteElements = new bool[levelsCount];

        if (resourceIdentifiers.Length != 0)
        {
            for (int level = 0; level < levelsCount; level++)
                ShowLevel(level);

            DeleteElement();
            AddLevelButton();
        }
        else
        {
            GUILayout.BeginHorizontal();
            TEditor.HorizontalBorder();
            GUILayout.Label("Please create Resources System component and mark by Tag");
            GUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void ShowLevel(int level)
    {
        SerializedProperty currentLevel = levels.GetArrayElementAtIndex(level);
        SerializedProperty priceResource = currentLevel.FindPropertyRelative("priceResource");
        SerializedProperty price = currentLevel.FindPropertyRelative("price");
        SerializedProperty inputResource = currentLevel.FindPropertyRelative("inputResource");
        SerializedProperty inputCount = currentLevel.FindPropertyRelative("inputCount");
        SerializedProperty outputResource = currentLevel.FindPropertyRelative("outputResource");
        SerializedProperty outputCount = currentLevel.FindPropertyRelative("outputCount");

        GUILayout.BeginVertical(GUI.skin.button);
        TEditor.VerticalBorder();

        if (level == 1)
        {
            this.priceResource = priceResource.stringValue;
            this.inputResource = inputResource.stringValue;
            this.outputResource = outputResource.stringValue;
        }

        if (level == 0)
        {
            // Title
            GUILayout.BeginHorizontal();
            TEditor.HorizontalBorder();
            GUILayout.Label("Build price", TEditor.SubTitleStyle());
            GUILayout.EndHorizontal();
            TEditor.Space();

            // Price
            GUILayout.BeginHorizontal();
            TEditor.HorizontalBorder();
            price.intValue = EditorGUILayout.IntField(price.intValue, GUILayout.Width(TEditor.FieldWidth));
            TEditor.Tab();
            int index = TEditor.GetResourceIndex(priceResource.stringValue, resourceIdentifiers);
            index = EditorGUILayout.Popup(index, resourceIdentifiers, GUILayout.Width(TEditor.FieldWidth));
            priceResource.stringValue = resourceIdentifiers[index];
            GUILayout.EndHorizontal();
        }
        else
        {
            // Title
            GUILayout.BeginHorizontal();
            TEditor.HorizontalBorder();
            GUILayout.Label("Level " + level, TEditor.SubTitleStyle());

            // Delete
            if (level >= 2)
            {
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Delete", GUILayout.Width(TEditor.TextWidth)))
                    deleteElements[level] = true;
                GUI.backgroundColor = defaultColor;
            }
            GUILayout.EndHorizontal();
            TEditor.Space();

            // Price
            GUIStyle customStyle = TEditor.SubTitleStyle();
            customStyle.fontSize = 12;

            GUILayout.BeginHorizontal();
            TEditor.HorizontalBorder();
            GUILayout.Label("Price", customStyle);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            TEditor.HorizontalBorder();
            price.intValue = EditorGUILayout.IntField(price.intValue, GUILayout.Width(TEditor.FieldWidth));
            if (level <= 1 || priceResourceSame.boolValue == false)
            {
                TEditor.Tab();
                int priceIndex = TEditor.GetResourceIndex(priceResource.stringValue, resourceIdentifiers);
                priceIndex = EditorGUILayout.Popup(priceIndex, resourceIdentifiers, GUILayout.Width(TEditor.FieldWidth));
                priceResource.stringValue = resourceIdentifiers[priceIndex];
            }
            else
            {
                priceResource.stringValue = this.priceResource;
            }
            GUILayout.EndHorizontal();

            TEditor.Space();

            // Input
            if (haveInput.boolValue)
            {
                GUILayout.BeginHorizontal();
                TEditor.HorizontalBorder();
                GUILayout.Label("Input Resource", customStyle);
                GUILayout.EndHorizontal();

                
                GUILayout.BeginHorizontal();
                TEditor.HorizontalBorder();
                inputCount.intValue = EditorGUILayout.IntField(inputCount.intValue, GUILayout.Width(TEditor.FieldWidth));
                if (level == 1 || inputResourceSame.boolValue == false)
                {
                    TEditor.Tab();
                    int inputIndex = TEditor.GetResourceIndex(inputResource.stringValue, resourceIdentifiers);
                    inputIndex = EditorGUILayout.Popup(inputIndex, resourceIdentifiers, GUILayout.Width(TEditor.FieldWidth));
                    inputResource.stringValue = resourceIdentifiers[inputIndex];
                }
                else
                {
                    inputResource.stringValue = this.inputResource;
                }
                GUILayout.EndHorizontal();

                TEditor.Space();
            }

            // Output
            GUILayout.BeginHorizontal();
            TEditor.HorizontalBorder();
            GUILayout.Label("Output Resource", customStyle);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            TEditor.HorizontalBorder();
            outputCount.intValue = EditorGUILayout.IntField(outputCount.intValue, GUILayout.Width(TEditor.FieldWidth));
            if (level == 1 || outputResourceSame.boolValue == false)
            {
                TEditor.Tab();
                int outputIndex = TEditor.GetResourceIndex(outputResource.stringValue, resourceIdentifiers);
                outputIndex = EditorGUILayout.Popup(outputIndex, resourceIdentifiers, GUILayout.Width(TEditor.FieldWidth));
                outputResource.stringValue = resourceIdentifiers[outputIndex];
            }
            else
            {
                outputResource.stringValue = this.outputResource;
            }
            GUILayout.EndHorizontal();
        }

        TEditor.VerticalBorder();
        GUILayout.EndVertical();
        TEditor.Space();
    }

    private void DeleteElement()
    {
        for (int index = 0; index < deleteElements.Length; index++)
            if (deleteElements[index])
            {
                levels.DeleteArrayElementAtIndex(index);
                levelsCount = levels.arraySize;
            }
    }

    private void AddLevelButton()
    {
        GUIStyle buttonText = new GUIStyle(GUI.skin.button);
        buttonText.fontStyle = FontStyle.BoldAndItalic;
        buttonText.normal.textColor = Color.white;
        buttonText.fontSize = 14;

        if (GUILayout.Button("Add Level", buttonText, GUILayout.Height(70)))
            levels.InsertArrayElementAtIndex(levelsCount);
    }
}