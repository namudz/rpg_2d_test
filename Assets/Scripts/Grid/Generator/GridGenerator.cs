using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : IGridGenerator 
{
    private readonly GridConfig _gridConfig;
    private readonly float _cellOffset;
    private readonly int _counterCharacters;
    private readonly int _counterEnemies;

    public GridGenerator(GridConfig gridConfig, int counterCharacters, int counterEnemies)
    {
        _gridConfig = gridConfig;
        _cellOffset = gridConfig.CellOffset;
        _counterCharacters = counterCharacters;
        _counterEnemies = counterEnemies;
    }

    public IGrid GenerateGrid()
    {
        var rows = _gridConfig.GetRows();
        var columns = _gridConfig.GetColumns();

        var cellsById = new Dictionary<int, Cell>(rows * columns);
        var cellsByCoordinates = new Cell[rows, columns];
        var cellId = 0;
        for (var i = 0; i < rows; ++i)
        {
            for (var j = 0; j < columns; ++j)
            {
                var cell = GetNewCell(rows, columns, cellId, j, i);
                cellsById.Add(cellId, cell);
                ++cellId;
                cellsByCoordinates[i, j] = cell;
            }
        }

        var data = new GridGenerationData(rows, columns, cellsById, cellsByCoordinates, _counterCharacters, _counterEnemies);
        return new Grid(data);
    }

    private Cell GetNewCell(int rows, int columns, int id, int x, int y)
    {
        var coordinates = new CellCoordinates(x, y);
        var worldPosition = new Vector2(x * _cellOffset, y * _cellOffset);
        var adjacentCells = GetAdjacentCellsToCell(rows, columns, id, x, y);
        return new Cell(id, coordinates, worldPosition, adjacentCells);
    }

    private int[] GetAdjacentCellsToCell(int rows, int columns, int cellId, int x, int y)
    {
        var adjacentCellsIds = new int[4]
        {
            y + 1 < rows ? cellId + columns: -1, // North
            x + 1 < columns ? cellId + 1 : -1, // East
            y - 1 >= 0 ? cellId - columns: -1, // South
            x - 1 >= 0 ? cellId - 1: -1 // Weast
        };
        
        return adjacentCellsIds;
    }
}
