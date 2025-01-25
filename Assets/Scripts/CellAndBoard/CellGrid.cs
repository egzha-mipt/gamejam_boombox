using UnityEngine;

public class CellGrid : MonoBehaviour
{
    public GameObject tileType1; 
    public GameObject tileType2;
    public int rows = 20;
    public int columns = 5;
    public float tileSize = 1.4f;

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
                GameObject tilePrefab = Random.Range(0, 2) == 0 ? tileType1 : tileType2;

                GameObject tile = Instantiate(tilePrefab, new Vector3(x * tileSize, y * tileSize, 0), Quaternion.identity);

                tile.transform.parent = this.transform;
            }
        }
    }
}
