using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeHint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public bool Focused { get; private set; }

    private RectTransform rect;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector2(rect.rect.width, rect.rect.height);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Focused = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Focused = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Focused = false;
    }
}
