using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid : IGrid
{
    public int Rows {get; private set;}
    public int Columns {get; private set;}    
    private readonly Dictionary<int, Cell> _cellsById;
    private readonly Cell[,] _cellsByCoordinates;
    private readonly List<AUnitController> _charactersOnGrid;
    private readonly List<AUnitController> _enemiesOnGrid;
    private readonly IAdjacentCellsFinder _adjacentCellsFinder;

    public Grid(GridGenerationData data)
    {
        Rows = data.Rows;
        Columns = data.Columns;
        _cellsById = data.CellsById;
        _cellsByCoordinates = data.CellsByCoordinates;

        _charactersOnGrid = new List<AUnitController>(data.CounterCharacters);
        _enemiesOnGrid = new List<AUnitController>(data.CounterEnemies);
        _adjacentCellsFinder = new AdjacentCellsFinder();
    }

    public Cell GetCellById(int cellId)
    {
        if (_cellsById.ContainsKey(cellId))
        {
            return _cellsById[cellId];
        }
        return null;
    }

    public Cell GetRandomFreeCell()
    {
        var occupiedCellsIds = new int[_charactersOnGrid.Count + _enemiesOnGrid.Count];
        var counterOccupiedCells = 0;
        for(var i = 0; i < _charactersOnGrid.Count; ++i)
        {
            occupiedCellsIds[counterOccupiedCells] = _charactersOnGrid[i].GetCurrentCellId();
            ++counterOccupiedCells;
        }
        for(var i = 0; i < _enemiesOnGrid.Count; ++i)
        {
            occupiedCellsIds[counterOccupiedCells] = _enemiesOnGrid[i].GetCurrentCellId();
            ++counterOccupiedCells;
        }

        var availableCells = _cellsById.Keys.ToList();
        for(var i = 0; i < counterOccupiedCells; ++ i)
        {
            availableCells.Remove(occupiedCellsIds[i]);
        }

        if (availableCells.Count == 0)
        {
            throw new System.IndexOutOfRangeException("No free cells left!");
        }

        var randomIndex = Random.Range(0, availableCells.Count);
        return GetCellById(availableCells[randomIndex]);
    }

    public Cell TryGetSpawnCell(int x, int y)
    {
        var cell = GetCellByCoordinates(x, y);
        var isCharacterOnCell = GetCharacterOnCell(cell.Id) != null;
        if (!isCharacterOnCell)
        {
            var isEnemyOnCell = GetEnemyOnCell(cell.Id) != null;
            if (!isEnemyOnCell)
            {
                return cell;
            }
        }

        return GetRandomFreeCell();
    }

    public Cell[] GetRowCells(int rowId)
    {
        if (rowId >= Rows) { return new Cell[0]; }
        
        var rowCells = new Cell[Columns];
        for(var i = 0; i < Columns; ++i)
        {
            rowCells[i] = GetCellByCoordinates(rowId, i);
        }
        return rowCells;
    }

    public bool IsCellFree(int cellId)
    {
        if (_charactersOnGrid.Find(unit => unit.GetCurrentCellId() == cellId)) return false;
        if (_enemiesOnGrid.Find(unit => unit.GetCurrentCellId() == cellId)) return false;
        return true;
    }

    public void AddCharacter(AUnitController unitController)
    {
        _charactersOnGrid.Add(unitController);
    }

    public void RemoveCharacter(AUnitController unitController)
    {
        _charactersOnGrid.Remove(unitController);
    }

    public void AddEnemy(AUnitController unitController)
    {
        _enemiesOnGrid.Add(unitController);
    }

    public void RemoveEnemy(AUnitController unitController)
    {
        _enemiesOnGrid.Remove(unitController);
    }

    public List<int> GetAdjacentCellsWithinRange(int cellId, int range)
    {
        return _adjacentCellsFinder.FindAdjacentCells(this, cellId, range);
    }

    public AUnitController GetCharacterOnCell(int cellId)
    {
        return _charactersOnGrid.Find(unit => unit.GetCurrentCellId() == cellId);
    }
    public AUnitController GetEnemyOnCell(int cellId)
    {
        return _enemiesOnGrid.Find(unit => unit.GetCurrentCellId() == cellId);
    }

    public List<AUnitController> GetAllCharacters()
    {
        return new List<AUnitController>(_charactersOnGrid);
    }

    private Cell GetCellByCoordinates(int x, int y)
    {
        if (x >= Rows || x < 0
            || y >= Columns || y < 0)
        {
            throw new System.IndexOutOfRangeException("Cell not found! Given coordinates = " + x + "," + y + " - Grid Size = " + Rows + " x " + Columns);
        }
        return _cellsByCoordinates[x, y];
    }
}
