using UnityEngine;
using UnityEngine.EventSystems;

public class HorizontalJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform handle;
    public float maxRadius = 80f;
    public float Value { get; private set; }

    RectTransform baseRect;
    Vector2 startPos;

    void Awake()
    {
        baseRect = GetComponent<RectTransform>();
        startPos = handle.anchoredPosition;
        Value = 0f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            baseRect,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
        );

        float dx = localPoint.x - startPos.x;
        dx = Mathf.Clamp(dx, -maxRadius, maxRadius);

        handle.anchoredPosition = new Vector2(startPos.x + dx, startPos.y);
        Value = dx / maxRadius;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        handle.anchoredPosition = startPos;
        Value = 0f;
    }
}
