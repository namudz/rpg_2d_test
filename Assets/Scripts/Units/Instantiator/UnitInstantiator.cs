using System.Collections.Generic;
using UnityEngine;

public class UnitInstantiator : IUnitInstantiator 
{
    private List<InstancedUnit> _poolUnits;

    public UnitInstantiator()
    {
        _poolUnits = new List<InstancedUnit>(20);
    }

    public AUnitController InstantiateUnit(GameObject prefab, Transform parent)
    {
        var pooledGameObject = TryGetFroomPool(prefab.GetInstanceID());
        if (pooledGameObject != null)
        {
            return pooledGameObject.GetComponent<AUnitController>();
        }

        var gameObject = GameObject.Instantiate (prefab, parent);
        var unitController = gameObject.GetComponent<AUnitController>();
        _poolUnits.Add(
            new InstancedUnit(prefab.GetInstanceID(), unitController)
        );
           
        return unitController;
    }

    private AUnitController TryGetFroomPool(int prefabInstanceId)
    {
        var instances = _poolUnits.FindAll(unit => unit.PrefabInstanceId == prefabInstanceId);
        if (instances.Count == 0) return null;

        for (var i = 0; i < instances.Count; ++i)
        {
            var unitController = instances[i].UnitController;
            if (unitController.CanBePooled)
            {
                return unitController;
            }
        }

        return null;
    }
}
