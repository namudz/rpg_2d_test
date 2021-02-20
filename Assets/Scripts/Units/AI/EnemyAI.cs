
public class EnemyAI : AUnitAI
{
    public EnemyAI(Unit unit, IGrid grid, IUnitMovement unitMovement) : base(unit, grid, unitMovement)
    {
    }

    public override void Move()
    {
        var destinationCellId = FindWeakestTargetCellWithinAttackRange();
        if (destinationCellId > -1)
        {
            _unitMovement.MoveTo(destinationCellId);
            return;
        }

        destinationCellId = FindCellWithinRangeToClosestTarget();
        _unitMovement.MoveTo(destinationCellId);
    }

    private int FindWeakestTargetCellWithinAttackRange()
    {
        var targetCellId = -1;
        var targetsOnGrid = _grid.GetAllCharacters();
        var lowestTargetHp = float.MaxValue;

        for (var i = 0; i < targetsOnGrid.Count; ++i)
        {
            var targetCellCoords = targetsOnGrid[i].GetCurrentCellCoordinates();
            var isWithinAttackRange = _unit.CurrentCell.Coordinates.AreCoordinatesInRange(targetCellCoords, _unit.AttackRange);
            if (isWithinAttackRange)
            {
                var targetCurrentHp = targetsOnGrid[i].GetCurrentHp();
                if (targetCurrentHp < lowestTargetHp)
                {
                    lowestTargetHp = targetCurrentHp;
                    targetCellId = targetsOnGrid[i].GetCurrentCellId();
                }
            }
        }

        return targetCellId;
    }

    private int FindCellWithinRangeToClosestTarget()
    {
        var closestTargetCellCoordinates = GetClosestTargetCellCoordinates();    
        var destinationCellId = _unit.CurrentCell.Id;
        var distanceToClosestTarget = int.MaxValue;
        
        var cellsIdsWithinMovementRange = _unitMovement.CellsIdsWithinMovementRange;
        for (var i = 0; i < cellsIdsWithinMovementRange.Count; ++i)
        {
            var cellId = cellsIdsWithinMovementRange[i];
            if (!_grid.IsCellFree(cellId)) continue;

            var cellCordinates = _grid.GetCellById(cellId).Coordinates;
            var distanceToTarget = cellCordinates.GetDistanceToCoordinates(closestTargetCellCoordinates);
            if (distanceToTarget < distanceToClosestTarget)
            {
                distanceToClosestTarget = distanceToTarget;
                destinationCellId = cellId;
            }
        }

        return destinationCellId;
    }

    private CellCoordinates GetClosestTargetCellCoordinates()
    {
        var targetsOnGrid = _grid.GetAllCharacters();
        var closestDistanceToTarget = int.MaxValue;
        var closestTargetCellCoordinates = _unit.CurrentCell.Coordinates;
        
        for(var i = 0; i < targetsOnGrid.Count; ++i)
        {
            var distanceToTarget = _unit.CurrentCell.Coordinates.GetDistanceToCoordinates(targetsOnGrid[i].GetCurrentCellCoordinates());
            if (distanceToTarget < closestDistanceToTarget)
            {
                closestDistanceToTarget = distanceToTarget;
                closestTargetCellCoordinates = targetsOnGrid[i].GetCurrentCellCoordinates();
            }
        }

        return closestTargetCellCoordinates;
    }
}