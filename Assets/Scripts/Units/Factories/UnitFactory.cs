using UnityEngine.Assertions;

public class UnitFactory : IUnitFactory 
{
    public Unit GetUnit(int unitId, UnitStatsConfig stats)
    {
        Assert.IsNotNull(stats, "Unit stats could't be null!");
        return new Unit(unitId, stats);
    }
}