using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public int x;
    public int y;

    public bool[] walls = { true, true, true, true };
    public bool isVisited = false;

    public GameObject cellPrefab;
    public List<Cell> cellNeighbors = new List<Cell>();
    public Cell[] cellDirections = new Cell[4];

    public void ActivateWalls()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            GameObject wall = cellPrefab.transform.GetChild(1).GetChild(i).gameObject;
            wall.SetActive(false);
        }
    }

    public Cell CalculateNeighbor()
    {
        if (WallIndex(x, y - 1) != -1)
        {
            cellDirections[0] = MazeGenerator.cellsInGrid[WallIndex(x, y - 1)];
        }

        if (WallIndex(x + 1, y) != -1)
        {
            cellDirections[1] = MazeGenerator.cellsInGrid[WallIndex(x + 1, y)];
        }

        if (WallIndex(x, y + 1) != -1)
        {
            cellDirections[2] = MazeGenerator.cellsInGrid[WallIndex(x, y + 1)];
        }

        if (WallIndex(x - 1, y) != -1)
        {
            cellDirections[3] = MazeGenerator.cellsInGrid[WallIndex(x - 1, y)];
        }

        for (int i = 0; i < walls.Length; i++)
        {
            if (cellDirections[i] != null && !cellDirections[i].isVisited)
            {
                cellNeighbors.Add(cellDirections[i]);
            }
        }

        if (cellNeighbors.Count > 0)
        {
            var random = Random.Range(0, cellNeighbors.Count);

            return cellNeighbors[random];
        }
        else
        {
            return null;
        }
    }

    public void RemoveWalls()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            cellPrefab.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
        }
    }

    public int WallIndex(int a, int b)
    {
        if (a < 0 || b < 0 || a > MazeGenerator.gridX - 1 || b > MazeGenerator.gridY - 1)
        {
            return -1;
        }

        return a + b * MazeGenerator.gridX;
    }
}
