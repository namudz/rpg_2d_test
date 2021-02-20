public class EnemyAttack : AUnitAttack
{
    public EnemyAttack(Unit unit, IGrid grid) : base(unit, grid)
    {
    }

    protected override AUnitController GetTargetController(int targetCellId)
    {
        return _grid.GetCharacterOnCell(targetCellId);
    }
}