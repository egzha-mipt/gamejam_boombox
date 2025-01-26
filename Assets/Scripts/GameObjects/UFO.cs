using UnityEngine;
using System.Collections;


public class UFO : MonoBehaviour
{
    public float speed = 3f; // Скорость движения НЛО
    public float lifetime = 15f; // Время жизни НЛО
    public GameObject dangerZonePrefab; // Префаб круга
    public float dangerZoneDiameter = 3f; // Диаметр круга
    public float minSpawnTime = 2f; // Минимальное время до появления круга
    public float maxSpawnTime = 10f; // Максимальное время до появления круга
    private float initialY = 12f; // Начальная Y-позиция для синусоиды
    private float timeElapsed = 0f; // Счётчик времени
    public float amplitude = 2f; // Амплитуда синусоиды
    public float frequency = 1f; // Частота синусоиды

    private bool hasSpawnedDangerZone = false; // Флаг, чтобы круг появлялся только один раз

    void Start()
    {
        Destroy(gameObject, lifetime); // Уничтожаем НЛО через заданное время
        StartCoroutine(SpawnDangerZoneOnce()); // Запускаем корутину для единственного появления круга
    }

    void Update()
    {
        // Двигаем НЛО слева направо
        float newY = initialY + Mathf.Sin(timeElapsed * frequency) * amplitude;
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0); // Горизонтальное движение
        transform.position = new Vector3(transform.position.x, newY, transform.position.z); // Обновление Y
        // transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    IEnumerator SpawnDangerZoneOnce()
    {
        // Ждём случайный промежуток времени перед созданием круга
        float randomDelay = Random.Range(minSpawnTime, maxSpawnTime);
        yield return new WaitForSeconds(randomDelay);

        // Создаём круг только если он ещё не был создан
        if (!hasSpawnedDangerZone && dangerZonePrefab != null)
        {
            hasSpawnedDangerZone = true; // Отмечаем, что круг уже создан
            Vector3 offset = new Vector3(0f, Random.Range(-2f, 2f), 0f); // Зона чуть выше или ниже
            GameObject dangerZone = Instantiate(dangerZonePrefab, transform.position + offset, Quaternion.identity);
            dangerZone.transform.localScale = new Vector3(dangerZoneDiameter, dangerZoneDiameter, 1f);
        }
    }
}
