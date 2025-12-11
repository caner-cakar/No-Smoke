using UnityEngine;

public class CameraMobileMove : MonoBehaviour
{
    public MobileJoystick moveJoystick;
    public float moveSpeed = 2f;

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

        Vector3 moveDir = forward * input.y + right * input.x;

        if (moveDir.sqrMagnitude < 0.0001f)
            return;

        moveDir.Normalize();
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
