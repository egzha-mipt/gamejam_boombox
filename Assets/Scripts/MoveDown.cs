using UnityEngine;

public class MoveDown : MonoBehaviour
{
    private float speed = 2f; // Скорость движения вниз

    void Update()
    {
        // Двигаем объект вниз по оси Y
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}