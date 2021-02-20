using System.Collections.Generic;

public interface IUnitSpawner
{
    List<AUnitController> SpawnUnits(IGrid grid, InitialUnitConfig[] unitsConfigs);
}