using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tileType1;
    public GameObject tileType2;
    public GameObject tileType3;
    public GameObject tileType4;

    public int rows = 20;
    public int columns = 5;
    public float tileSize = 1f;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject tilePrefab = GetRandomTile();

                GameObject tile = Instantiate(tilePrefab, new Vector3(x * tileSize, y * tileSize, 0), Quaternion.identity);

                tile.transform.parent = this.transform;
            }
        }
    }

    GameObject GetRandomTile()
    {
        int randomIndex = Random.Range(0, 4);

        switch (randomIndex)
        {
            case 0:
                return tileType1;
            case 1:
                return tileType2;
            case 2:
                return tileType3;
            case 3:
                return tileType4;
            default:
                return tileType1;
        }
    }
}
