using UnityEngine;

public class CameraZoomGesture : MonoBehaviour
{
    public Camera targetCamera;
    public float mouseZoomSpeed = 10f;
    public float touchZoomSpeed = 0.1f;
    public float minZoom = 20f;
    public float maxZoom = 60f;

    void Awake()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;
    }

    void Update()
    {
        if (targetCamera == null)
            return;

        float scroll = Input.mouseScrollDelta.y;
        if (Mathf.Abs(scroll) > 0.001f)
        {
            if (targetCamera.orthographic)
            {
                float size = targetCamera.orthographicSize;
                size -= scroll * mouseZoomSpeed * Time.deltaTime;
                size = Mathf.Clamp(size, minZoom, maxZoom);
                targetCamera.orthographicSize = size;
            }
            else
            {
                float fov = targetCamera.fieldOfView;
                fov -= scroll * mouseZoomSpeed;
                fov = Mathf.Clamp(fov, minZoom, maxZoom);
                targetCamera.fieldOfView = fov;
            }
        }

        if (Input.touchCount >= 2)
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            Vector2 prevPos0 = t0.position - t0.deltaPosition;
            Vector2 prevPos1 = t1.position - t1.deltaPosition;

            float prevMag = (prevPos0 - prevPos1).magnitude;
            float currentMag = (t0.position - t1.position).magnitude;
            float deltaMag = currentMag - prevMag;

            if (Mathf.Abs(deltaMag) > 0.01f)
            {
                if (targetCamera.orthographic)
                {
                    float size = targetCamera.orthographicSize;
                    size -= deltaMag * touchZoomSpeed * Time.deltaTime;
                    size = Mathf.Clamp(size, minZoom, maxZoom);
                    targetCamera.orthographicSize = size;
                }
                else
                {
                    float fov = targetCamera.fieldOfView;
                    fov -= deltaMag * touchZoomSpeed;
                    fov = Mathf.Clamp(fov, minZoom, maxZoom);
                    targetCamera.fieldOfView = fov;
                }
            }
        }
    }
}
