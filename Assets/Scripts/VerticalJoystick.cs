using UnityEngine;
using UnityEngine.EventSystems;

public class VerticalJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform handle; 
    public float maxRadius = 80f; 

    public float Value { get; private set; }

    RectTransform _baseRect;
    Vector2 _startPos;

    void Awake()
    {
        _baseRect = GetComponent<RectTransform>();
        _startPos = handle.anchoredPosition;
        Value = 0f;
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

        float dy = localPoint.y - _startPos.y;
        dy = Mathf.Clamp(dy, -maxRadius, maxRadius);

        handle.anchoredPosition = new Vector2(_startPos.x, _startPos.y + dy);

        Value = dy / maxRadius;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        handle.anchoredPosition = _startPos;
        Value = 0f;
    }
}
