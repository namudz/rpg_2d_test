using System.Linq;
using UnityEngine;

public class Cell
{
    public int Id {get; private set;}
    public CellCoordinates Coordinates {get; private set;}
    public Vector2 WorldPosition {get; private set;}
    private readonly int[] _adjacentCellsIds;

    public Cell(int id, CellCoordinates coordinates, Vector2 worldPosition, int[] adjacentCellsIds)
    {
        Id = id;
        Coordinates = coordinates;
        WorldPosition = worldPosition;
        _adjacentCellsIds = adjacentCellsIds;
    }

    public int[] GetAdjacentCellsIds()
    {
        return _adjacentCellsIds.Where(cellId => cellId > -1).ToArray();
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        
        var cell = (Cell) obj;
        return Id == cell.Id && Coordinates.Equals(cell.Coordinates);
    }

    public override string ToString()
    {
        var adjacentCellsText = "";
        for(var i = 0; i < _adjacentCellsIds.Length; ++i)
        {
            adjacentCellsText += _adjacentCellsIds[i];
            if (i < _adjacentCellsIds.Length - 1)
            {
                adjacentCellsText += ", ";
            }
        }

        var text = string.Format(
            "Id = {0} - Coordinates = {1} - WorldPosition = {2} - AdjacentCells = {3}",
            Id,
            Coordinates.ToString(),
            WorldPosition.ToString(),
            adjacentCellsText
        );
        return text;
    }
}