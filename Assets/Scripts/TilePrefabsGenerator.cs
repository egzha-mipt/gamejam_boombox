using System.IO;
using UnityEditor;
using UnityEngine;

public class TilePrefabsGenerator : MonoBehaviour
{
    public GameObject[] tileTypes; // Массив с типами тайлов (почтовый тайл на позиции 2)
    public int rows = 5; // Количество строк
    public int cols = 8; // Количество столбцов
    public float height = 1.44f; // Высота тайла
    public float width = 1.44f; // Ширина тайла

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
            // Создаём пустой родительский объект
            GameObject parent = new GameObject($"GeneratedTile_{i}");
            parent.transform.position = Vector3.zero;

            // Генерация тайлов для текущего префаба
            GameObject[,] generatedTiles = new GameObject[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    // Выбираем случайный тайл
                    GameObject tilePrefab = ChooseTilePrefab();

                    // Создаём тайл
                    GameObject tile = Instantiate(tilePrefab, parent.transform);
                    tile.transform.localPosition = new Vector3(col * width, row * height, 0); // Расположение относительно родителя

                    // Сохраняем ссылку на тайл
                    generatedTiles[row, col] = tile;
                }
            }

            // Обязательно добавляем почтовый тайл на случайную позицию
            PlaceGuaranteedTile(generatedTiles, parent);

            // Сохраняем объект как префаб
#if UNITY_EDITOR
            string prefabPath = $"{savePath}/GeneratedTile_{i}.prefab";
            PrefabUtility.SaveAsPrefabAsset(parent, prefabPath);
#endif
            Destroy(parent); // Уничтожаем временный объект
        }

        Debug.Log($"Сгенерировано {prefabsToGenerate} префабов в папке {savePath}");
    }

    GameObject ChooseTilePrefab()
    {
        // Устанавливаем вероятность для выбора разных типов тайлов
        float randomValue = Random.value;

        if (randomValue > 0.1f) // 50% вероятность для первого типа
        {
            return tileTypes[0];
        }
        else // Остальные 50% для второго типа
        {
            return tileTypes[1];
        }
    }

    void PlaceGuaranteedTile(GameObject[,] tiles, GameObject parent)
    {
        // Выбираем случайную позицию в сетке
        int randomRow = Random.Range(0, rows);
        int randomCol = Random.Range(0, cols);

        // Уничтожаем тайл, если он уже есть на этой позиции
        if (tiles[randomRow, randomCol] != null)
        {
            DestroyImmediate(tiles[randomRow, randomCol]);
        }

        // Устанавливаем почтовый тайл
        GameObject mailTile = Instantiate(tileTypes[2], parent.transform);
        mailTile.transform.localPosition = new Vector3(randomCol * width, randomRow * height, 0);
        tiles[randomRow, randomCol] = mailTile;

        Debug.Log($"Почтовый тайл размещён на позиции: [{randomRow}, {randomCol}]");
    }
}
