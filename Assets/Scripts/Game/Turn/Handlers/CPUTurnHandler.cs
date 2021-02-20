using System.Threading.Tasks;

public class CPUTurnHandler : ATurnHandler
{
    private int _nextUnitToMoveIndex;

    public CPUTurnHandler()
    {
        _myTurnType = TurnTypes.Turn.CPU;
    }

    protected override void ResetTurn()
    {
        _nextUnitToMoveIndex = 0;
        _unitsToMove = _unitsControllers.Count;
        if (_unitsToMove > 0)
        {
            MoveNextUnit();
        }
    }

    protected override void HandleUnitMovementFinished()
    {
        --_unitsToMove;
        if (_unitsToMove == 0)
        {
            TurnFinished();
            return;
        }

        ++_nextUnitToMoveIndex;
        if (_nextUnitToMoveIndex < _unitsControllers.Count)
        {
            MoveNextUnit();
        }
    }

    private async void MoveNextUnit()
    {
        // Waiting .5s to simulate cpu is thinking
        await Task.Delay(500);
        _unitsControllers[_nextUnitToMoveIndex].UnitMovement.SetCanMove(true);
        _unitsControllers[_nextUnitToMoveIndex].UnitAI.Move();
    }
}