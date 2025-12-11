using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public Vector3 minBounds = new Vector3(-10f, 1f, -10f); 
    public Vector3 maxBounds = new Vector3( 10f, 8f,  10f);

    void LateUpdate()
    {
        Vector3 p = transform.position;

        p.x = Mathf.Clamp(p.x, minBounds.x, maxBounds.x);
        p.y = Mathf.Clamp(p.y, minBounds.y, maxBounds.y);
        p.z = Mathf.Clamp(p.z, minBounds.z, maxBounds.z);

        transform.position = p;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = (minBounds + maxBounds) * 0.5f;
        Vector3 size   = (maxBounds - minBounds);
        Gizmos.DrawWireCube(center, size);
    }
}
