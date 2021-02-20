public class InstancedUnit
{
    public int PrefabInstanceId {get; private set;}
    public AUnitController UnitController {get; private set;}

    public InstancedUnit(int prefabInstanceId, AUnitController unitController)
    {
        PrefabInstanceId = prefabInstanceId;
        UnitController = unitController;
    }
}