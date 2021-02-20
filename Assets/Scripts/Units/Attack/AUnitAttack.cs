public abstract class AUnitAttack : IUnitAttack 
{
    private readonly Unit _unit;
    protected readonly IGrid _grid;

    public AUnitAttack(Unit unit, IGrid grid)
    {
        _unit = unit;
        _grid = grid;
    }

    public bool Attack(int targetCellId)
    {
        var target = GetTargetController(targetCellId);
        if (target == null) return false;

        var command = new DamageUnitCommand(target.UnitHealth, _unit.AttackPoints);
        command.Execute();

        return true;
    }

    protected abstract AUnitController GetTargetController(int targetCellId);
}