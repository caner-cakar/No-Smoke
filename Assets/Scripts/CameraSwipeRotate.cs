using UnityEngine;
using UnityEngine.EventSystems;

public class CameraSwipeRotate : MonoBehaviour
{
    public float touchRotateSpeed = 0.01f;
    public float mouseRotateSpeed = 0.2f;

    public RectTransform[] ignoreZones;
    public Camera uiCamera;

    bool rotatingWithMouse;
    Vector2 lastMousePos;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            rotatingWithMouse = true;
            lastMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1))
        {
            rotatingWithMouse = false;
        }
        if (rotatingWithMouse)
        {
            Vector2 curPos = Input.mousePosition;
            float dx = curPos.x - lastMousePos.x;
            if (Mathf.Abs(dx) > 0.01f)
            {
                transform.Rotate(Vector3.up, dx * mouseRotateSpeed * Time.deltaTime, Space.World);
            }
            lastMousePos = curPos;
        }

        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);

            if (IsInIgnoreZone(t.position))
                return;

            if (t.phase == TouchPhase.Moved)
            {
                float dx = t.deltaPosition.x;
                if (Mathf.Abs(dx) > 0.01f)
                {
                    transform.Rotate(Vector3.up, dx * touchRotateSpeed, Space.World);
                }
            }
        }
    }

    bool IsInIgnoreZone(Vector2 screenPos)
    {
        if (ignoreZones == null)
            return false;

        for (int i = 0; i < ignoreZones.Length; i++)
        {
            RectTransform r = ignoreZones[i];
            if (r == null) continue;

            if (RectTransformUtility.RectangleContainsScreenPoint(r, screenPos, uiCamera))
                return true;
        }

        return false;
    }
}
