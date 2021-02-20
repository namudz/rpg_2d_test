using System.Collections.Generic;
using System.Linq;

public class AdjacentCellsFinder : IAdjacentCellsFinder 
{
    public List<int> FindAdjacentCells(IGrid grid, int cellId, int range)
    {
        var totalAdjacentCellsIds = new List<int>(grid.Rows * grid.Columns);
        
        GetAdjacentCellsWithinRangeRecursive(grid, cellId, range, totalAdjacentCellsIds);
        totalAdjacentCellsIds = totalAdjacentCellsIds.Distinct().ToList();
        totalAdjacentCellsIds.Remove(cellId);

        return totalAdjacentCellsIds;
    }

    private void GetAdjacentCellsWithinRangeRecursive(IGrid grid, int cellId, int range, List<int> totalAdjacentCellsIds)
    {
        if (range > 0)
        {
            var cell = grid.GetCellById(cellId);
            var adjacentCellsIds = cell.GetAdjacentCellsIds();
            totalAdjacentCellsIds.AddRange(adjacentCellsIds);
            for(var i = 0; i < adjacentCellsIds.Length; ++i)
            {
               GetAdjacentCellsWithinRangeRecursive(grid, adjacentCellsIds[i], range - 1, totalAdjacentCellsIds);
            }
        }
    }
}