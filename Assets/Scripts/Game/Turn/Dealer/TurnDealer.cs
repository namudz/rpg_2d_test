using System.Collections.Generic;

public class TurnDealer : ITurnDealer
{
    public event System.Action<TurnTypes.Turn> OnTurnChanged;
    private readonly ATurnHandler _playerTurnHandler;
    private readonly ATurnHandler _cpuTurnHandler;
    private TurnTypes.Turn _currentTurn;

    public TurnDealer(ATurnHandler playerTurnHandler, ATurnHandler cpuTurnHandler)
    {
        _playerTurnHandler = playerTurnHandler;
        _cpuTurnHandler = cpuTurnHandler;

        _currentTurn = TurnTypes.Turn.Player;

        _playerTurnHandler.InjectDependencies(this);
        _playerTurnHandler.OnTurnFinished += ChangeTurnToCPU;
        _cpuTurnHandler.InjectDependencies(this);
        _cpuTurnHandler.OnTurnFinished += ChangeTurnToPlayer;
    }

    public void SetCurrentTurn(TurnTypes.Turn newTurn)
    {
        _currentTurn = newTurn;
        OnTurnChanged?.Invoke(_currentTurn);
    }

    public void SetCharacterUnits(List<AUnitController> unitsControllers)
    {
        _playerTurnHandler.SetTurnUnitControllers(unitsControllers);
    }
    public void SetEnemyUnits(List<AUnitController> unitsControllers)
    {
        _cpuTurnHandler.SetTurnUnitControllers(unitsControllers);
    }

    private void ChangeTurnToCPU()
    {
        SetCurrentTurn(TurnTypes.Turn.CPU);
    }

    private void ChangeTurnToPlayer()
    {
        SetCurrentTurn(TurnTypes.Turn.Player);
    }
}
