using System.Collections.Generic;
using UnityEngine;

public abstract class AUnitSpawner : IUnitSpawner
{
    private readonly IUnitFactory _unitFactory;
    private readonly IUnitInstantiator _unitInstatiator;
    private readonly Transform _unitsParent;
    private readonly ITurnDealer _turnDealer;
    private List<AUnitController> _unitsSpawned;
    protected IGrid _grid;

    public AUnitSpawner(Transform unitsParent, ITurnDealer turnDealer)
    {
        _unitFactory = new UnitFactory();
        _unitInstatiator = new UnitInstantiator();
        _unitsParent = unitsParent;
        _turnDealer = turnDealer;
    }

    public List<AUnitController> SpawnUnits(IGrid grid, InitialUnitConfig[] unitsConfigs)
    {
        _grid = grid;
        _unitsSpawned = new List<AUnitController>(unitsConfigs.Length);
        for (var i = 0; i < unitsConfigs.Length; ++i)
        {
            var unitConfig = unitsConfigs[i];
            var unit = GetNewUnit(i, unitConfig, grid);
            
            var unitController = _unitInstatiator.InstantiateUnit(unitConfig.UnitConfig.Prefab, _unitsParent);
            var unitAttack = GetUnitAttack(unit, grid);
            var unitHealth = new UnitHealth(unit);
            var unitMovement = new UnitMovement(unitController.transform, unit, grid, unitAttack);
            var unitAI = GetUnitAI(unit, grid, unitMovement);
            unitController.InjectDependencies(unit, _turnDealer, grid, unitHealth, unitMovement, unitAI);
            unitController.InjectMonobehaviourDependencies();
            unitController.Spawn();
            unitController.UnitHealth.OnDeath += ReleaseUnit;
            _unitsSpawned.Add(unitController);
        }

        return _unitsSpawned;
    }

    protected abstract IUnitAttack GetUnitAttack(Unit unit, IGrid grid);
    protected abstract IUnitAI GetUnitAI(Unit unit, IGrid grid, IUnitMovement unitMovement);

    private Unit GetNewUnit(int unitId, InitialUnitConfig initialUnitConfig, IGrid grid)
    {
        var spawnCell = GetSpawnCell(grid, initialUnitConfig);
        var unit = _unitFactory.GetUnit(unitId, initialUnitConfig.UnitConfig);
        unit.CurrentCell = spawnCell;
        return unit;
    }

    private Cell GetSpawnCell(IGrid grid, InitialUnitConfig unitConfig)
    {
        if (unitConfig.SpawnConfig.Randomly)
        {
            return grid.GetRandomFreeCell();
        }
        else
        {
            var cell = unitConfig.SpawnConfig.Cell;
            return grid.TryGetSpawnCell(cell.x, cell.y);
        }
    }

    private void ReleaseUnit(int unitId)
    {
        var unitController = _unitsSpawned.Find(unit => unit.GetUnitId() == unitId);
        if (unitController != null)
        {
            unitController.UnitHealth.OnDeath -= ReleaseUnit;
            _unitsSpawned.Remove(unitController);
            RemoveUnitFromGrid(unitController);
            if (_unitsSpawned.Count == 0)
            {
                LaunchGameOverSignal();
            }
            return;
        }
    }

    protected abstract void RemoveUnitFromGrid(AUnitController unitController);
    protected abstract void LaunchGameOverSignal();
}