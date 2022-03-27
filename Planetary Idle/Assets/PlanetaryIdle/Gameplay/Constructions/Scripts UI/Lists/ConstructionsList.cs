using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionsList : MonoBehaviour
{
    private static ConstructionsList Instance;

    [Header("View")]
    [SerializeField] private GameObject content;
    //[SerializeField] private TouchArea touchArea;

    private ConstructionPlace selectedPlace;

    public static ConstructionPlace SelectedPlace { get { return Instance.selectedPlace; } private set { } }
    //public static bool Touched { get { return Instance.touchArea.Touched; } private set { } }

    private void Awake()
    {
        Instance = this;
        content.SetActive(false);
    }

    public static void ShowView(ConstructionPlace selectedPlace)
    {
        View.HideAll();
        Instance.content.SetActive(true);
        Instance.selectedPlace = selectedPlace;
    }

    public static void HideView()
    {
        Instance.content.SetActive(false);
    }
}
