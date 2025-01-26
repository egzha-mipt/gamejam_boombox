using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public float speed = 5f; // Начальная скорость круга
    public float minDirectionMagnitude = 0.1f; // Минимальная длина вектора направления
    private Transform player;
    private Vector3 direction;

    void Start()
    {
        // Ищем игрока
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player != null)
        {
            // Рассчитываем направление к игроку
            direction = (player.position - transform.position).normalized;

            // Проверяем длину вектора направления
            if ((player.position - transform.position).magnitude < minDirectionMagnitude)
            {
                Debug.LogWarning("Игрок слишком близко. Устанавливаю направление вниз по умолчанию.");
                direction = Vector3.down; // Устанавливаем дефолтное направление (вниз)
            }
        }
        else
        {
            Debug.LogWarning("Игрок не найден! Убедитесь, что объект с игроком имеет тег 'Player'.");
            direction = Vector3.down; // Если игрок не найден, по умолчанию направление вниз
        }

        Destroy(gameObject, 20f); // Уничтожаем объект через 20 секунд
    }

    void Update()
    {
        if (player != null)
        {
            // Двигаем объект в направлении игрока
            transform.Translate(direction * speed * Time.deltaTime);

            // Постепенное увеличение скорости
            speed += 1f * Time.deltaTime; // При необходимости можно настроить прирост скорости
        }
    }
}
