using System.IO;
using UnityEditor;
using UnityEngine;

public class TilePrefabsGenerator1 : MonoBehaviour
{
    public GameObject tileType1; // ������ ������� ���� ������
    public GameObject[] tileType2; // ������ ������� ���� ������
    public int rows = 5; // ���������� �����
    public int cols = 8; // ���������� �������
    public float height;
    public float width;


    public int prefabsToGenerate = 10; // ������� �������� �������������
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
            // ������ ������������ ������
            GameObject parent = new GameObject($"GeneratedTile_{i}");
            parent.transform.position = Vector3.zero;

            // ��������� �������
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    // �������� ��������� ��� �����
                    GameObject tilePrefab = Random.value > 0.2f ? tileType1 : tileType2[Random.Range(0, tileType2.Length)];

                    // ������ ����
                    GameObject tile = Instantiate(tilePrefab, parent.transform);
                    tile.transform.position = new Vector3(col*width, row * height, 0); // ����������� � �����
                }
            }

            // ��������� ��� ������
#if UNITY_EDITOR
            string prefabPath = $"{savePath}/GeneratedTile_{i}.prefab";
            PrefabUtility.SaveAsPrefabAsset(parent, prefabPath);
#endif
            Destroy(parent); // ������� �� �����
        }

        Debug.Log($"������������� {prefabsToGenerate} �������� � ����� {savePath}");
    }
}