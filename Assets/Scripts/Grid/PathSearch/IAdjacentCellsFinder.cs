using System.Collections.Generic;

public interface IAdjacentCellsFinder
{
    List<int> FindAdjacentCells(IGrid grid, int cellId, int range);
}