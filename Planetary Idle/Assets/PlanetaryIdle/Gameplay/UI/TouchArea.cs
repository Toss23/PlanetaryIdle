using UnityEngine;
using UnityEngine.EventSystems;

public class TouchArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool Touched { get; private set; }
    public int TouchID { get; private set; }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Touched = false;
            TouchID = -1;
            OnHoldingEnd();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        foreach (Touch touch in Input.touches)
            if (touch.phase == TouchPhase.Began)
                TouchID = touch.fingerId;

        Touched = true;
        OnHoldingBegin();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Touched = false;
        OnHoldingEnd();
        TouchID = -1;
    }

    protected virtual void OnHoldingBegin() { }

    protected virtual void OnHoldingEnd() { }
}