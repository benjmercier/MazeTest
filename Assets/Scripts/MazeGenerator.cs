using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private int _gridX = 20,
        _gridY = 20;

    public static int gridX, gridY;

    private Vector2 _mazeStart = new Vector2(0f, 0f);

    [SerializeField]
    private GameObject _cellPrefab;
    private Cell _currentCell;
    private Cell _nextCell;


    public static List<Cell> cellsInGrid = new List<Cell>();
    public static List<Cell> cellsInStack = new List<Cell>();
    public static List<Cell> correctCellPath = new List<Cell>();

    private int pathIndex = 0;
    private bool isCorrectPath = false;

    private void Start()
    {
        gridX = _gridX;
        gridY = _gridY;

        GenerateMaze(_gridX, _gridY);

        CalculateMaze();
    }

    private void GenerateMaze(int width, int height)
    {
        for (int a = 0; a < width; a++)
        {
            for (int b = 0; b < height; b++)
            {
                Cell cell = new Cell();
                cell.x = b;
                cell.y = a;

                cellsInGrid.Add(cell);
            }
        }

        for (int i = 0; i < cellsInGrid.Count; i++)
        {
            Vector3 pos = new Vector3(cellsInGrid[i].x, 0, cellsInGrid[i].y);

            cellsInGrid[i].cellPrefab = Instantiate(_cellPrefab, pos, Quaternion.identity);
        }

        _currentCell = cellsInGrid[0];
    }

    private void CalculateMaze()
    {
        _currentCell.isVisited = true;

        _nextCell = _currentCell.CalculateNeighbor();
        _currentCell.cellNeighbors = new List<Cell>();

        if (_nextCell != null)
        {
            _nextCell.isVisited = true;

            cellsInStack.Add(_currentCell);

            RemoveCellWalls(_currentCell, _nextCell);

            _currentCell = _nextCell;
        }
        else if (cellsInStack.Count > 0)
        {
            _currentCell = cellsInStack[cellsInStack.Count - 1];
            cellsInStack.RemoveAt(cellsInStack.Count - 1);
        }
        else
        {

        }
    }

    private void RemoveCellWalls(Cell current, Cell next)
    {
        int x = current.x - next.x;

        if (x == 1)
        {
            current.walls[0] = false;
            next.walls[2] = false;
        }
        else if (x == -1)
        {
            current.walls[2] = false;
            next.walls[0] = false;
        }

        int y = current.y - next.y;
        if (y == 1)
        {
            current.walls[3] = false;
            next.walls[1] = false;
        }
        else if (y == -1)
        {
            current.walls[1] = false;
            next.walls[3] = false;
        }

        current.ActivateWalls();
        next.ActivateWalls();
    }
}
