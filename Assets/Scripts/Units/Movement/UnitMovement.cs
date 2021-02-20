using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : IUnitMovement
{
    public event System.Action OnMovementFinished;
    public event System.Action OnAdjacentCellsUpdated;

    public List<int> CellsIdsWithinMovementRange {get; private set;}
    public List<int> CellsIdsWithinAttackRange {get; private set;}
    private readonly Transform _transform;
    private readonly Unit _unit;
    private readonly IGrid _grid;
    private readonly IUnitAttack _unitAttack;
    private bool _canMove;

    public UnitMovement(Transform unitTransform, Unit unit, IGrid grid, IUnitAttack unitAttack)
    {
        _transform = unitTransform;
        _unit = unit;
        _grid = grid;
        _unitAttack = unitAttack;
    }

    public void SetCanMove(bool canMove)
    {
        _canMove = canMove;
    }

    public void MoveTo(int targetCellId)
    {
        if (!_canMove) return;

        var isWithinMovementRange = CellsIdsWithinMovementRange.Contains(targetCellId);
        var isWithinAttackRange = CellsIdsWithinAttackRange.Contains(targetCellId);
        if (!isWithinMovementRange && !isWithinAttackRange)
        {
            if (targetCellId == _unit.CurrentCell.Id)
            {
                MovementFinished(_unit.CurrentCell);
            }
            return;
        }

        if (isWithinAttackRange)
        {
            var canAttack = _unitAttack.Attack(targetCellId);
            if (canAttack)
            {
                MovementFinished(_unit.CurrentCell, false);
                return;
            }
        }

        if (!isWithinMovementRange) return;

        // At this point I would have ask to injected IPathFinder for a path.
        // And instead of direclty teleport to the target cell, move using DOTween through each cell of the path.

        var isPathAvailable = _grid.IsCellFree(targetCellId);
        if (isPathAvailable)
        {
            Teleport(_grid.GetCellById(targetCellId));
        }

    }

    public void Teleport(Cell targetCell)
    {
        _transform.position = targetCell.WorldPosition;
        MovementFinished(targetCell);
    }

    private void MovementFinished(Cell newCell, bool updateAdjacentCells = true)
    {
        _unit.CurrentCell = newCell;
        OnMovementFinished?.Invoke();
        if (updateAdjacentCells)
        {
            UpdateAdjacentCells();
        }
    }

    private void UpdateAdjacentCells()
    {
        CellsIdsWithinMovementRange = _grid.GetAdjacentCellsWithinRange(_unit.CurrentCell.Id, _unit.MovementRange);
        CellsIdsWithinAttackRange = _grid.GetAdjacentCellsWithinRange(_unit.CurrentCell.Id, _unit.AttackRange);
        OnAdjacentCellsUpdated?.Invoke();
    }
}