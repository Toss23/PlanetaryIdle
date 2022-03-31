using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ConstructionElement : MonoBehaviour
{
    [Header("Construction")]
    [SerializeField] protected Construction construction;

    [Header("Texts")]
    [SerializeField] protected Text nameText;
    [SerializeField] protected Text tierText;
    [SerializeField] protected Text priceText;

    [Header("Image")]
    [SerializeField] protected Image elementImage;
    [SerializeField] protected Sprite spriteForImage;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClickElement);
        UpdateMainView();
        UpdateView();
    }

    private void UpdateMainView()
    {
        nameText.text = "Name: " + construction.ConstructionName;
        tierText.text = "Tier: " + construction.Tier;
        priceText.text = "Price: " + construction.RequireCount + " " + construction.RequireName;

        if (spriteForImage != null) elementImage.sprite = spriteForImage;
    }

    protected abstract void UpdateView();

    private void OnClickElement()
    {
        /*
        if (ResourcesHolder.GetValue(construction.RequireName) >= construction.RequireCount)
        {
            ResourcesHolder.AddValue(construction.RequireName, - construction.RequireCount);
            ConstructionsList.HideView();
            ConstructionsList.SelectedPlace.Build(construction);
        }
        */
    }
}
