using UnityEngine;
using UnityEngine.EventSystems;

public class VerticalJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform handle;
    public float maxRadius = 80f;

    public float Value { get; private set; }

    RectTransform baseRect;

    void Awake()
    {
        baseRect = GetComponent<RectTransform>();
        if (handle != null)
            handle.anchoredPosition = Vector2.zero;
        Value = 0f;
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

        float dy = localPoint.y;
        dy = Mathf.Clamp(dy, -maxRadius, maxRadius);

        handle.anchoredPosition = new Vector2(0f, dy);
        Value = dy / maxRadius;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (handle == null)
            return;

        handle.anchoredPosition = Vector2.zero;
        Value = 0f;
    }
}
