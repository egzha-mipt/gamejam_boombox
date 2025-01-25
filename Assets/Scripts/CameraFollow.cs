using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform father;
    public float smoothSpeed = 0.125f;
    private float fixedX;

    void Start()
    {
        fixedX = transform.position.x;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(fixedX, father.position.y, transform.position.z);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
