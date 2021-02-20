using UnityEngine;

public class CharacterSpawner : AUnitSpawner 
{
    public CharacterSpawner(Transform unitsParent, ITurnDealer turnDealer) : base(unitsParent, turnDealer)
    {
    }

    protected override IUnitAttack GetUnitAttack(Unit unit, IGrid grid)
    {
        return new CharacterAttack(unit, grid);
    }

    protected override IUnitAI GetUnitAI(Unit unit, IGrid grid, IUnitMovement unitMovement)
    {
        return null;
    }

    protected override void RemoveUnitFromGrid(AUnitController unitController)
    {
        _grid.RemoveCharacter(unitController);
    }

    protected override void LaunchGameOverSignal()
    {
        GameOverSignal.Instance.LaunchSignal(TurnTypes.Turn.CPU);
    }
}