using UnityEngine;

public class GridRowController : MonoBehaviour
{
    [SerializeField] private CellController[] _cells;

    private int _activeCells;

    public void SetUpCells(Cell[] rowCells)
    {
        _activeCells = rowCells.Length;
        for(var i = 0; i < _cells.Length; ++i)
        {
            _cells[i].SetUp(i < rowCells.Length ? rowCells[i] : null);
        }
    }

    public void SetHandleInput(bool toHandleInput)
    {
        for(var i = 0; i < _activeCells; ++i)
        {
            _cells[i].SetHandleInput(toHandleInput);
        }
    }
}