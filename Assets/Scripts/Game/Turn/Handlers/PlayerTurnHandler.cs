public class PlayerTurnHandler : ATurnHandler
{
    private TurnTypes.PlayerTurnStates _turnState;

    public PlayerTurnHandler()
    {
        _myTurnType = TurnTypes.Turn.Player;
        CharacterSelectedSignal.OnCharacterSelected += HandleCharacterSelected;
        CharacterUnselectedSignal.OnCharacterUnselected += HandleCharacterUnselected;
    }

    protected override void ResetTurn()
    {
        _unitsToMove = _unitsControllers.Count;
        UpdateTurnState(TurnTypes.PlayerTurnStates.Select);
    }

    private void HandleCharacterSelected(int characterInstanceId)
    {
        UpdateTurnState(TurnTypes.PlayerTurnStates.Action);
    }

    private void HandleCharacterUnselected()
    {
        UpdateTurnState(TurnTypes.PlayerTurnStates.Select);
    }

    private void UpdateTurnState(TurnTypes.PlayerTurnStates newState)
    {
        _turnState = newState;
        PlayerTurnStateSignal.Instance.LaunchSignal(_turnState);
    }

    protected override void HandleUnitMovementFinished()
    {
        --_unitsToMove;
        if (_unitsToMove == 0)
        {
            TurnFinished();
            return;
        }
        UpdateTurnState(TurnTypes.PlayerTurnStates.Select);
    }
}