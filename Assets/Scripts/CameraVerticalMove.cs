using UnityEngine;

public class CameraVerticalMove : MonoBehaviour
{
    public MobileJoystick verticalJoystick; 

    public float verticalSpeed = 3f;  

    public bool useHeightLimits = true;
    public float minY = 1f;
    public float maxY = 10f;

    void Update()
    {
        if (verticalJoystick == null)
            return;

        Vector2 input = verticalJoystick.Value;

        float yDir = input.y;

        if (Mathf.Abs(yDir) < 0.01f)
            return;

        Vector3 pos = transform.position;
        pos.y += yDir * verticalSpeed * Time.deltaTime;

        if (useHeightLimits)
        {
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
        }

        transform.position = pos;
    }
}
