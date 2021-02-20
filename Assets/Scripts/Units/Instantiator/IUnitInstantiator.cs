using UnityEngine;

public interface IUnitInstantiator
{
    AUnitController InstantiateUnit(GameObject prefab, Transform parent);
}
