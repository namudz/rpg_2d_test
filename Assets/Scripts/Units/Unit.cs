public class Unit
{
    public int InstanceId {get; protected set;}
    public HealthPoints HealthPoints;
    public int MovementRange {get; protected set;}
    public int AttackRange {get; protected set;}
    public float AttackPoints {get; protected set;}
    public Cell CurrentCell;

    public Unit(int id, UnitStatsConfig stats)
    {
        InstanceId = id;
        HealthPoints = new HealthPoints(stats.HealthPoints);
        MovementRange = stats.MovementRange;
        AttackRange = stats.AttackRange;
        AttackPoints = stats.AttackPoints;
    }
}