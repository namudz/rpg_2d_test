using UnityEngine;

public class EnemySpawner : AUnitSpawner 
{
    public EnemySpawner(Transform unitsParent, ITurnDealer turnDealer) : base(unitsParent, turnDealer)
    {
    }

    protected override IUnitAttack GetUnitAttack(Unit unit, IGrid grid)
    {
        return new EnemyAttack(unit, grid);
    }

    protected override IUnitAI GetUnitAI(Unit unit, IGrid grid, IUnitMovement unitMovement)
    {
        return new EnemyAI(unit, grid, unitMovement);
    }

    protected override void RemoveUnitFromGrid(AUnitController unitController)
    {
        _grid.RemoveEnemy(unitController);
    }

    protected override void LaunchGameOverSignal()
    {
        GameOverSignal.Instance.LaunchSignal(TurnTypes.Turn.Player);
    }
}