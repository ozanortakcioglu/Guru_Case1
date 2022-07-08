using UnityEngine;
using System.Collections.Generic;


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
        GetGridCell(gridPos).GetComponent<GridCell>().FillCell();
        CheckAndDeactivateCells(gridPos);
    }

    private void CheckAndDeactivateCells(Vector2Int lastActivatedGridPos)
    {
        List<Vector2Int> connectedCells = new List<Vector2Int>();
        connectedCells.Add(lastActivatedGridPos);

        while (true)
        {
            var initCount = connectedCells.Count;
            List<Vector2Int> newOnes = new List<Vector2Int>();

            foreach (var connected in connectedCells)
            {
                foreach (var neighbor in getFullNeighbors(connected))
                {
                    if (!connectedCells.Contains(neighbor) && !newOnes.Contains(neighbor))
                    {
                        newOnes.Add(neighbor);
                    }
                }
            }

            connectedCells.AddRange(newOnes);

            if (initCount == connectedCells.Count)
                break;
        }

        if(connectedCells.Count > 2)
        {
            foreach (var item in connectedCells)
            {
                GetGridCell(item).ResetCell();
                //Increase score
            }
        }
    }

    private List<Vector2Int> getFullNeighbors(Vector2Int gridPos)
    {
        List<Vector2Int> fullNeighbors = new List<Vector2Int>();
        

        if (gridPos.y + 1 < widthAndWeight)
        {
            var cell = GetGridCell(gridPos + new Vector2Int(0, 1));
            if (cell.isFull)
                fullNeighbors.Add(cell.GetPosition());
        }

        if (gridPos.y - 1 >= 0)
        {
            var cell = GetGridCell(gridPos + new Vector2Int(0, -1));
            if (cell.isFull)
                fullNeighbors.Add(cell.GetPosition());
        }

        if (gridPos.x + 1 < widthAndWeight)
        {
            var cell = GetGridCell(gridPos + new Vector2Int(1, 0));
            if (cell.isFull)
                fullNeighbors.Add(cell.GetPosition());
        }

        if (gridPos.x - 1 >= 0)
        {
            var cell = GetGridCell(gridPos + new Vector2Int(-1, 0));
            if (cell.isFull)
                fullNeighbors.Add(cell.GetPosition());
        }

        return fullNeighbors;
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


