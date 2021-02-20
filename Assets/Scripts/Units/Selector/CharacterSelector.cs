using System.Collections.Generic;

public class CharacterSelector : ICharacterSelector
{
    public bool IsSelectable {get; set;}
    private readonly Unit _unit;
    private readonly IUnitMovement _unitMovement;
    private readonly IUnitHealth _unitHealth;
    private List<int> _cellsWithinMovementRange;
    private List<int> _cellsWithinAttackRange;
    private bool _isSelected;

    public CharacterSelector(Unit unit, IUnitMovement unitMovement, IUnitHealth unitHealth)
    {
        _unit = unit;
        _unitMovement = unitMovement;
        _unitHealth = unitHealth;

        _unitMovement.OnMovementFinished += HandleMovementFinished;
        _unitMovement.OnAdjacentCellsUpdated += UpdateAdjacentCells;
        _unitHealth.OnDeath += UnsuscribeFromEvents;

        _isSelected = false;
    }

    public bool Select()
    {
        _isSelected = !_isSelected;
        if (_isSelected) {
            CharacterSelectedSignal.Instance.LaunchSignal(_unit.InstanceId);
        }
        else {
            CharacterUnselectedSignal.Instance.LaunchSignal();
        }

        LaunchSignalsToHighlightCells();

        return _isSelected;
    }

    public void Reset()
    {
        _isSelected = false;
        LaunchSignalsToHighlightCells();
        IsSelectable = false;
    }

    private void LaunchSignalsToHighlightCells()
    {
        if (_unit.AttackRange > _unit.MovementRange)
        {
            HighlightCellsSignal.Instance.LaunchSignal(_isSelected, _cellsWithinAttackRange, CellHighlightTypes.HighlightType.Attack);
            HighlightCellsSignal.Instance.LaunchSignal(_isSelected, _cellsWithinMovementRange, CellHighlightTypes.HighlightType.Movement);
        }
        else
        {
            HighlightCellsSignal.Instance.LaunchSignal(_isSelected, _cellsWithinMovementRange, CellHighlightTypes.HighlightType.Movement);
            HighlightCellsSignal.Instance.LaunchSignal(_isSelected, _cellsWithinAttackRange, CellHighlightTypes.HighlightType.Attack);
        }
    }

    private void HandleMovementFinished()
    {
        if (_isSelected)
        {
            Reset();
        }
    }

    private void UpdateAdjacentCells()
    {
        _cellsWithinMovementRange = _unitMovement.CellsIdsWithinMovementRange;
        _cellsWithinAttackRange = _unitMovement.CellsIdsWithinAttackRange;
    }

    private void UnsuscribeFromEvents(int unitId)
    {
        _unitMovement.OnMovementFinished -= HandleMovementFinished;
        _unitMovement.OnAdjacentCellsUpdated -= UpdateAdjacentCells;
        _unitHealth.OnDeath -= UnsuscribeFromEvents;
    }
}