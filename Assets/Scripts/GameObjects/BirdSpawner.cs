using System.Collections;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab;      // Префаб птицы
    public Transform leftSpawnPoint;  // Точка появления птицы слева

    private IEnumerator WaitForEventManager()
    {
        while (EventManager.Instance == null)
        {
            yield return null; // Ждём следующего кадра
        }
        EventManager.Instance.OnPosteCellTriggered += SpawnBirdAfterDelay;
    }

    private void OnEnable()
    {
        StartCoroutine(WaitForEventManager());
        EventManager.Instance.OnPosteCellTriggered += SpawnBirdAfterDelay;
    }

    private void OnDisable()
    {
        // Отписываемся от события PosteCell
        EventManager.Instance.OnPosteCellTriggered -= SpawnBirdAfterDelay;
    }

    private void SpawnBirdAfterDelay(Vector2 posteCellPosition)
    {
        // Рандомное время задержки от 1 до 5 секунд
        float delay = Random.Range(1f, 5f);
        StartCoroutine(SpawnBirdCoroutine(delay));
    }

    private IEnumerator SpawnBirdCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Создаём птицу слева
        if (birdPrefab != null && leftSpawnPoint != null)
        {
            Instantiate(birdPrefab, leftSpawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Не назначен префаб птицы или точка появления!");
        }
    }
}
