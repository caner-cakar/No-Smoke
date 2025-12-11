using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public HorizontalJoystick rotateJoystick;
    public float rotateSpeed = 25f;

    void Update()
    {
        if (rotateJoystick == null)
            return;

        float x = rotateJoystick.Value;

        if (Mathf.Abs(x) < 0.01f)
            return;

        transform.Rotate(Vector3.up * x * rotateSpeed * Time.deltaTime, Space.World);
    }
}
