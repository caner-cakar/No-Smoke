using UnityEngine;

public class CameraVerticalMove : MonoBehaviour
{
    public VerticalJoystick verticalJoystick;

    public float verticalSpeed = 3f; 

    public bool useHeightLimits = true;
    public float minY = 1f;
    public float maxY = 10f;

    void Update()
    {
        if (verticalJoystick == null)
            return;

        float yInput = verticalJoystick.Value;

        if (Mathf.Abs(yInput) < 0.01f)
            return;

        Vector3 pos = transform.position;
        pos.y += yInput * verticalSpeed * Time.deltaTime;

        if (useHeightLimits)
        {
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
        }

        transform.position = pos;
    }
}
