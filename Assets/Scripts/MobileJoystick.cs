using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform handle;  
    public float maxRadius = 80f; 

    public Vector2 Value { get; private set; }

    RectTransform _baseRect;
    Vector2 _startPos;

    void Awake()
    {
        _baseRect = GetComponent<RectTransform>();
        _startPos = handle.anchoredPosition;
        Value = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _baseRect,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
        );

        Vector2 offset = localPoint - _startPos;

        if (offset.magnitude > maxRadius)
        {
            offset = offset.normalized * maxRadius;
        }

        handle.anchoredPosition = _startPos + offset;

        Value = offset / maxRadius;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        handle.anchoredPosition = _startPos;
        Value = Vector2.zero;
    }
}
