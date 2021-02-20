public class CharacterAttack : AUnitAttack
{
    public CharacterAttack(Unit unit, IGrid grid) : base(unit, grid)
    {
    }

    protected override AUnitController GetTargetController(int targetCellId)
    {
        return _grid.GetEnemyOnCell(targetCellId);
    }
}