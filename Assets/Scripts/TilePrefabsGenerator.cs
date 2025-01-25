using System.IO;
using UnityEditor;
using UnityEngine;

public class TilePrefabsGenerator : MonoBehaviour
{
    public GameObject tileType1; // Префаб первого типа тайлов
    public GameObject tileType2; // Префаб второго типа тайлов
    public int rows = 5; // Количество строк
    public int cols = 8; // Количество колонок
    public float height;
    public float width;


    public int prefabsToGenerate = 10; // Сколько префабов сгенерировать
    public string savePath = "Assets/Prefabs/GeneratedTiles";

    void Start()
    {
        GeneratePrefabs();
    }

    void GeneratePrefabs()
    {
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        for (int i = 0; i < prefabsToGenerate; i++)
        {
            // Создаём родительский объект
            GameObject parent = new GameObject($"GeneratedTile_{i}");
            parent.transform.position = Vector3.zero;

            // Заполняем тайлами
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    // Выбираем случайный тип тайла
                    GameObject tilePrefab = Random.value > 0.2f ? tileType1 : tileType2;

                    // Создаём тайл
                    GameObject tile = Instantiate(tilePrefab, parent.transform);
                    tile.transform.position = new Vector3(col*width, row * height, 0); // Расставляем в сетке
                }
            }

            // Сохраняем как префаб
#if UNITY_EDITOR
            string prefabPath = $"{savePath}/GeneratedTile_{i}.prefab";
            PrefabUtility.SaveAsPrefabAsset(parent, prefabPath);
#endif
            Destroy(parent); // Удаляем из сцены
        }

        Debug.Log($"Сгенерировано {prefabsToGenerate} префабов в папке {savePath}");
    }
}
