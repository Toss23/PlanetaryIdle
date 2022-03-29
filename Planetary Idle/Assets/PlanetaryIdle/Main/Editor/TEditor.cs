using System.Collections.Generic;
using UnityEngine;

public class TEditor
{
    public static int TextWidth = 85;
    public static int FieldWidth = 150;
    public static int ToggleWidth = 15;

    public static int space = 5;
    public static int border = 5;
    public static int tab = 5;

    public static void HorizontalBorder()
    {
        GUILayout.Label("", GUILayout.Width(border));
    }

    public static void VerticalBorder()
    {
        GUILayout.Space(border);
    }

    public static void HorizontalBorder(int count)
    {
        GUILayout.Label("", GUILayout.Width(border * count));
    }

    public static void VerticalBorder(int count)
    {
        GUILayout.Space(border * count);
    }

    public static void Tab()
    {
        GUILayout.Label("", GUILayout.Width(tab));
    }

    public static void Space()
    {
        GUILayout.Space(space);
    }

    public static void TitleSpace()
    {
        GUILayout.Space(space * 2);
    }

    public static GUIStyle TitleStyle()
    {
        GUIStyle mainTitle = new GUIStyle();
        mainTitle.fontStyle = FontStyle.BoldAndItalic;
        mainTitle.normal.textColor = Color.white;
        mainTitle.fontSize = 18;
        return mainTitle;
    }

    public static GUIStyle SubTitleStyle()
    {
        GUIStyle subTitle = new GUIStyle();
        subTitle.fontStyle = FontStyle.BoldAndItalic;
        subTitle.normal.textColor = Color.white;
        subTitle.fontSize = 16;
        return subTitle;
    }

    public static string[] ResourceIdentifiers()
    {
        string[] resourceIdentifiers = new string[0];

        GameObject gameObject = GameObject.FindWithTag("ResourcesSystem");
        if (gameObject != null)
        {
            ResourcesSystem resourcesSystem = gameObject.GetComponent<ResourcesSystem>();
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
                resourceIdentifiers[index] = identifiers[index];
        }

        return resourceIdentifiers;
    }

    public static int GetResourceIndex(string identifier, string[] resourceIdentifiers)
    {
        int index = 0;
        for (int i = 0; i < resourceIdentifiers.Length; i++)
        {
            if (resourceIdentifiers[i] == identifier)
            {
                index = i;
                break;
            }
        }
        return index;
    }
}
