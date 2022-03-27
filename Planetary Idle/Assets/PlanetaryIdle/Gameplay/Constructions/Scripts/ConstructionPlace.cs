using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider))]
public class ConstructionPlace : MonoBehaviour, IPointerClickHandler
{
    private GameObject construction;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (construction == null)
        {
            View.HideAll();
            ConstructionsList.ShowView(this);
        }
    }

    public void Build(Construction prefab)
    {
        if (construction == null)
        {
            construction = Instantiate(prefab.gameObject, transform);
            construction.name = prefab.ConstructionName;
        }
    }
}
