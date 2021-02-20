using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private GridRowController[] _rows;
    private ITurnDealer _iTurnDealer;

    public void InjectDependencies(ITurnDealer turnDealer)
    {
        _iTurnDealer = turnDealer;
        _iTurnDealer.OnTurnChanged += UpdateInputController;
    }

    public void Render(IGrid grid)
    {
        var gridRows = grid.Rows;
        for(var i = 0; i < _rows.Length; ++i)
        {
            _rows[i].SetUpCells(grid.GetRowCells(i));
        }
    }

    private void UpdateInputController(TurnTypes.Turn turn)
    {
        for(var i = 0; i < _rows.Length; ++i)
        {
            _rows[i].SetHandleInput(turn == TurnTypes.Turn.Player);
        }
    }
}
