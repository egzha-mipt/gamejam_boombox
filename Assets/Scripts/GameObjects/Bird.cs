using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public GameObject letterPrefab; 
    public Transform spawnPoint;
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 5f;
    public float speed = 1f;
    public float direction = 1f; // 1 - вправо, -1 - влево

    private void Start()
    {
        StartCoroutine(SpawnLetterCoroutine());
    }

    private IEnumerator SpawnLetterCoroutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);

            SpawnLetter();
        }
    }

    private void SpawnLetter()
    {
        if (letterPrefab != null && spawnPoint != null)
        {
            Instantiate(letterPrefab, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Problem in Bird.cs in SpawnLetter");
        }
    }

    private void Update()
    {
        transform.Translate(new Vector3(-1f * direction * speed * Time.deltaTime, 0, 0));        
    }
}
