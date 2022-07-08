using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    public GameObject gridCellPrefab;

    public int widthAndWeight;
    private float cellSize = 1;

    private GameObject[,] grid;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CreateGrid();
    }

    #region Controls
    public bool isOnTheGrid(Vector2Int gridPos)
    {
        if (gridPos.x >= 0 && gridPos.x < widthAndWeight && gridPos.y >= 0 && gridPos.y < widthAndWeight)
            return true;
        else
            return false;
    }

    public void ActivateGridCell(Vector2Int gridPos)
    {
        grid[gridPos.x, gridPos.y].GetComponent<GridCell>().FillCell();
    }


    #endregion

    #region Grid General

    public void RebuildGrid(int _size)
    {
        widthAndWeight = _size;
        CreateGrid();
    }
    private void CreateGrid()
    {
        grid = new GameObject[widthAndWeight, widthAndWeight];

        for (int y = 0; y < widthAndWeight; y++)
        {
            for (int x = 0; x < widthAndWeight; x++)
            {
                grid[x, y] = Instantiate(gridCellPrefab, new Vector3(x * cellSize, 0.01f, y * cellSize), Quaternion.identity);
                grid[x, y].GetComponent<GridCell>().SetPosition(x, y);
                grid[x, y].GetComponent<GridCell>().isFull = false;
                grid[x, y].transform.parent = transform;
                grid[x, y].gameObject.name = "GridCell (X: " + x + ", Y: " + y + ")";
            }
        }
    }

    public GridCell GetGridCell(Vector2Int gridPos)
    {
        return grid[gridPos.x, gridPos.y].GetComponent<GridCell>();
    }

    public Vector2Int GetGridPosFromWorld(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt(worldPos.x / cellSize);
        int y = Mathf.FloorToInt(worldPos.z / cellSize);

        return new Vector2Int(x, y);
    }

    public Vector3 GetWorldPosFromGridPos(Vector2Int gridPos, bool isMiddle)
    {
        float x = gridPos.x * cellSize;
        float z = gridPos.y * cellSize;

        if (isMiddle)
            return new Vector3(x + cellSize / 2, 0, z + cellSize / 2);
        else
            return new Vector3(x, 0, z);
    }
    #endregion

}


