public class CharacterUnitController : AUnitController 
{
    private bool _isSelected;
    private TurnTypes.PlayerTurnStates _playerTurnState;
    private CharacterSelector _unitSelector;

    protected override void InitializeDependencies()
    {
        _unitSelector = new CharacterSelector(_unit, UnitMovement, UnitHealth);
    }

    protected override void OnSpawn()
    {
        _isSelected = false;
        _grid.AddCharacter(this);
        SubscribeToSignals();
    }

    private void SubscribeToSignals()
    {
        CellClickedSignal.OnCellClicked += HandleCellClicked;
        PlayerTurnStateSignal.OnTurnStateUpdated += HandlePlayerTurnStateUpdated;
    }

    protected override void HandleTurnChanged(TurnTypes.Turn turn)
    {
        var isPlayerTurn = turn == TurnTypes.Turn.Player;
        _spriteController.SetIsSelectable(isPlayerTurn);
        _unitSelector.IsSelectable = isPlayerTurn;
        UnitMovement.SetCanMove(isPlayerTurn);
        
        if (!isPlayerTurn)
        {
            _isSelected = false;
            _unitSelector.Reset();
        }
    }

    private void HandleCellClicked(int cellId)
    {
        if (!_unitSelector.IsSelectable) return;

        if (_unit.CurrentCell.Id != cellId)
        {
            if (_isSelected && _playerTurnState == TurnTypes.PlayerTurnStates.Action)
            {
                UnitMovement.MoveTo(cellId);
                return;
            }
            return;
        }

        if (!_isSelected && _playerTurnState == TurnTypes.PlayerTurnStates.Action) return;

        _isSelected = _unitSelector.Select();
        _spriteController.SetIsSelectable(!_isSelected);
    }

    private void HandlePlayerTurnStateUpdated(TurnTypes.PlayerTurnStates turnState)
    {
        _playerTurnState = turnState;
    }

    protected override void UnsuscribeFromConcreteEvents()
    {
        CellClickedSignal.OnCellClicked -= HandleCellClicked;
        PlayerTurnStateSignal.OnTurnStateUpdated -= HandlePlayerTurnStateUpdated;
    }
}