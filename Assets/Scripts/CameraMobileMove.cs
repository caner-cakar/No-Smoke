using UnityEngine;

public class CameraMobileMove : MonoBehaviour
{
    [Header("Joystick")]
    public MobileJoystick moveJoystick;

    public float moveSpeed = 5f;

    void Update()
    {
        if (moveJoystick == null)
            return;

        Vector2 input = moveJoystick.Value;

        if (input.sqrMagnitude < 0.0001f)
            return;

        Vector3 forward = transform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = transform.right;
        right.y = 0f;
        right.Normalize();

        Vector3 dir = forward * input.y + right * input.x;

        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}
