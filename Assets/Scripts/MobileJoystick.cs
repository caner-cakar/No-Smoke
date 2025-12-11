using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform handle;
    public float maxRadius = 80f;
    public Vector2 Value { get; private set; }

    RectTransform baseRect;

    void Awake()
    {
        baseRect = GetComponent<RectTransform>();
        if (handle != null)
            handle.anchoredPosition = Vector2.zero;
        Value = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (handle == null)
            return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            baseRect,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
        );

        Vector2 offset = localPoint;

        if (offset.magnitude > maxRadius)
            offset = offset.normalized * maxRadius;

        handle.anchoredPosition = offset;
        Value = offset / maxRadius;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (handle == null)
            return;

        handle.anchoredPosition = Vector2.zero;
        Value = Vector2.zero;
    }
}
