using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider))]
public abstract class Construction : MonoBehaviour, IPointerClickHandler
{
    [Header("Main")]
    [SerializeField] protected string constructionName;
    [SerializeField] protected int tier;

    [Header("Require Resource")]
    [SerializeField] private string requireName = "Gold";
    [SerializeField] private int requireCount;

    // Getters and Setters
    public string ConstructionName { get { return constructionName; } private set { constructionName = value; } }
    public int Tier { get { return tier; } private set { tier = value; } }
    public string RequireName { get { return requireName; } private set { requireName = value; } }
    public int RequireCount { get { return requireCount; } private set { requireCount = value; } }

    public void OnPointerClick(PointerEventData eventData)
    {
        View.HideAll();
        ShowView();
    }

    protected abstract void ShowView();
}
