using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class View : MonoBehaviour
{
    private static List<View> Views;

    [Header("Default")]
    [SerializeField] private GameObject content;

    [Header("Buttons")]
    [SerializeField] private Button escapeButton;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button destroyButton;

    private void Awake()
    {
        // Add view in List<View>
        if (Views == null) Views = new List<View>();
        Views.Add(this);

        // Configurate Buttons
        escapeButton.onClick.AddListener(HideView);

        if (upgradeButton != null)
        {
            upgradeButton.onClick.AddListener(OnClickUpgrade);
            upgradeButton.onClick.AddListener(UpdateView);
        }

        if (destroyButton != null)
        {
            destroyButton.onClick.AddListener(OnClickDestroy);
            destroyButton.onClick.AddListener(HideView);
        }

        HideView();
        Initialize();
    }

    /// <summary>
    /// Realized after Awake
    /// </summary>
    protected abstract void Initialize();

    /// <summary>
    /// This method is Update and Show View
    /// </summary>
    public void ShowView()
    {
        UpdateView();
        content.SetActive(true);
    }

    private void HideView()
    {
        content.SetActive(false);
    }

    /// <summary>
    /// This method is called before Show View
    /// </summary>
    protected abstract void UpdateView();

    /// <summary>
    /// This method is called on click Upgrade Button
    /// </summary>
    protected abstract void OnClickUpgrade();

    /// <summary>
    /// This method is called on click Destroy Button
    /// </summary>
    protected abstract void OnClickDestroy();

    /// <summary>
    /// This method is Hide All Views
    /// </summary>
    public static void HideAll()
    {
        foreach (View view in Views)
        {
            view.HideView();
        }
    }
}
