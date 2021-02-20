using System.Collections.Generic;

public interface IUnitMovement
{
    event System.Action OnMovementFinished;
    event System.Action OnAdjacentCellsUpdated;

    List<int> CellsIdsWithinMovementRange {get;} 
    List<int> CellsIdsWithinAttackRange {get;}

    void SetCanMove(bool canMove);

    void Teleport(Cell targetCell);
    void MoveTo(int targetCellId);
}