using UnityEngine;

public class MoveDown : MonoBehaviour
{
    private float speed = 3f; // Скорость движения вниз

    void LateUpdate()
    {
        // Двигаем объект вниз по оси Y
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}