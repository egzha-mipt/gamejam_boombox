using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject[] tileArray;

    public int rows = 40;
    public int columns = 8;
    public float tileSize = 1f;

    public float speed = 1f;

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
                GameObject tilePrefab = tileArray[GetRandomTile()];

                GameObject tile = Instantiate(tilePrefab, new Vector3(x * tileSize, y * tileSize, 0), Quaternion.identity);

                tile.transform.parent = this.transform;
            }
        }
    }

    int GetRandomTile()
    {
        int randomIndex = Random.Range(0, tileArray.Length);

        return randomIndex;
    }

    void Update()
    {
        transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
    }
}
