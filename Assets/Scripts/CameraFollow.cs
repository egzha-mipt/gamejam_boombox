using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform father;
    public float smoothSpeed = 0.125f;
    public float CamDeflectionY = 3f;
    public float CamDeflectionX = 10f;
    private float fixedX;
    void Start()
    {
        fixedX = transform.position.x; 
    }

    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(fixedX + CamDeflectionX, father.position.y + CamDeflectionY, transform.position.z);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
