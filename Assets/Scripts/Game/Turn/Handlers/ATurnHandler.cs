using System.Collections.Generic;

public abstract class ATurnHandler
{
    public event System.Action OnTurnFinished;

    protected TurnTypes.Turn _myTurnType;
    protected ITurnDealer _turnDealer;
    protected List<AUnitController> _unitsControllers;
    protected int _unitsToMove;

    public void InjectDependencies(ITurnDealer turnDealer)
    {
        _turnDealer = turnDealer;
        _turnDealer.OnTurnChanged += OnTurnChanged;
    }

    public void SetTurnUnitControllers(List<AUnitController> unitsControllers)
    {
        _unitsControllers = unitsControllers;
        SubscribeToUnitEvents();
    }

    private void SubscribeToUnitEvents()
    {
        for(var i = 0; i < _unitsControllers.Count; ++i)
        {
            _unitsControllers[i].UnitMovement.OnMovementFinished += HandleUnitMovementFinished;
        }
    }

    private void OnTurnChanged(TurnTypes.Turn turn)
    {
        if (turn != _myTurnType)
        { 
            return;
        }
        ResetTurn();
    }

    protected void TurnFinished()
    {
        OnTurnFinished?.Invoke();
    }

    protected abstract void ResetTurn();

    protected abstract void HandleUnitMovementFinished();
}
