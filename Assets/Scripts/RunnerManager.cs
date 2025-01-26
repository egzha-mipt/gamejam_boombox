using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerManager : MonoBehaviour
{
    public string easyPrefabsPath = "GeneratedTiles/Easy"; // Путь к папке с простыми префабами
    public string hardPrefabsPath = "GeneratedTiles/Hard"; // Путь к папке со сложными префабами
    public string mailPrefabsPath = "GeneratedTiles/Mail";
    public float time; // Интервал между спавном новых префабов
    public float spawnOffset = 7.2f; // Отступ между спавнами префабов

    public Transform spawnPoint; // Точка появления префаба
    public Transform destroyPoint; // Точка, после которой префаб уничтожается

    private Queue<GameObject> activePrefabs = new Queue<GameObject>(); // Очередь активных префабов
    public List<GameObject> easyPrefabs = new List<GameObject>(); // Лист с простыми префабами
    public List<GameObject> mailPrefabs = new List<GameObject>();
    public List<GameObject> hardPrefabs = new List<GameObject>(); // Лист со сложными префабами
    private bool nextPrefabsIsMail = false;

    public void  NeedMailPrefab()
    {
        nextPrefabsIsMail = true;
    }

    void Start()
    {
        LoadPrefabs(easyPrefabsPath, easyPrefabs); // Загружаем префабы из папки Easy
        LoadPrefabs(hardPrefabsPath, hardPrefabs); // Загружаем префабы из папки Hard
        LoadPrefabs(mailPrefabsPath, mailPrefabs);

        // Спавним первые три префаба с отступами
        for (int i = 0; i < 3; i++)
        {
            Vector3 offsetPosition = spawnPoint.position ;
            offsetPosition.y= offsetPosition.y+ spawnOffset * i;
            SpawnPrefab(offsetPosition);
        }
    }

    void LoadPrefabs(string path, List<GameObject> prefabList)
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>(path);
        prefabList.AddRange(prefabs);

        Debug.Log($"Загружено {prefabs.Length} префабов из {path}");

        foreach (var prefab in prefabs)
        {
            Debug.Log($"Загружен префаб: {prefab.name}");
        }

        if (prefabs.Length == 0)
        {
            Debug.LogWarning($"Папка {path} пуста или путь указан неверно!");
        }
    }

    void SpawnPrefab(Vector3 position)
    {
        // Определяем сложность текущего префаба
        GameObject currentPrefab= Random.value > 0.3f ? easyPrefabs[Random.Range(0,easyPrefabs.Count)] :hardPrefabs[Random.Range(0,hardPrefabs.Count)];

        // Если выбранный пул пуст, переключаемся на другой
        //if (currentPool.Count == 0) currentPool = easyPrefabs;

        //if (currentPool.Count > 0)
        {


            // Создаём префаб в сцене
            GameObject newPrefab = Instantiate(currentPrefab, position, Quaternion.identity);
            activePrefabs.Enqueue(newPrefab);
        }
    }
    void SpawnMailPrefab(Vector3 position)
    {
        GameObject currentPrefab= mailPrefabs[Random.Range(0,mailPrefabs.Count)];
        GameObject newPrefab = Instantiate(currentPrefab, position, Quaternion.identity);
        activePrefabs.Enqueue(newPrefab);

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            NeedMailPrefab();
        }
        if (activePrefabs.Count > 0)
        {
            GameObject firstPrefab = activePrefabs.Peek(); // Берём первый префаб в очереди

            // Проверяем, если он пересёк destroyPoint
            if (firstPrefab.transform.position.y < destroyPoint.position.y)
            {
                if (nextPrefabsIsMail){
                    SpawnMailPrefab(spawnPoint.position);
                    nextPrefabsIsMail=false;
                }
                else{
                    SpawnPrefab(spawnPoint.position);
                }
                
                activePrefabs.Dequeue(); // Убираем из очереди
                Destroy(firstPrefab); // Уничтожаем объект
                
                // Спавним новый префаб вместо удалённого
                
            }
        }
    }
}