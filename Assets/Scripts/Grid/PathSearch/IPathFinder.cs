public interface IPathFinder
{
    int[] FindPathBetweenCells(IGrid grid, int startCellId, int targetCellId);
}