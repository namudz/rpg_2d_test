public abstract class AUnitAI : IUnitAI 
{
    protected readonly Unit _unit;
    protected readonly IGrid _grid;
    protected readonly IUnitMovement _unitMovement;

    public AUnitAI(Unit unit, IGrid grid, IUnitMovement unitMovement)
    {
        _unit = unit;
        _grid = grid;
        _unitMovement = unitMovement;
    }

    public abstract void Move();
}