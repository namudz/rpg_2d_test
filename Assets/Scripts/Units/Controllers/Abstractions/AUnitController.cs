using UnityEngine;

public abstract class AUnitController : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    [Header ("Controllers")]
    [SerializeField] protected UnitSpriteController _spriteController;
    [SerializeField] protected UnitCanvasView _canvasView;
    [SerializeField] protected UnitFxController _fxController;

    public IUnitMovement UnitMovement {get; private set;}
    public IUnitHealth UnitHealth {get; private set;}
    public IUnitAI UnitAI {get; private set;}

    public bool CanBePooled {get; private set;}

    protected Unit _unit;
    protected IGrid _grid;
    private ITurnDealer _turnDealer;

    public int GetUnitId()
    {
        return _unit.InstanceId;
    }

    public int GetCurrentCellId()
    {
        return _unit.CurrentCell.Id;
    }

    public CellCoordinates GetCurrentCellCoordinates()
    {
        return _unit.CurrentCell.Coordinates;
    }

    public float GetCurrentHp()
    {
        return _unit.HealthPoints.Current;
    }

    public void InjectDependencies(
        Unit unit, 
        ITurnDealer turnDealer, 
        IGrid grid, 
        IUnitHealth unitHealth,
        IUnitMovement unitMovement,
        IUnitAI unitAI)
    {
        _unit = unit;
        _turnDealer = turnDealer;
        _grid = grid;
        UnitMovement = unitMovement;
        UnitHealth = unitHealth;
        UnitAI = unitAI;

        _turnDealer.OnTurnChanged += HandleTurnChanged;
        UnitHealth.OnDeath += HandleUnitDeath;
        GameOverSignal.OnGameOver += HandleGameOver;
        ResetGameSignal.OnGameReset += ResetUnit;
        InitializeDependencies();
    }

    public void InjectMonobehaviourDependencies()
    {
        _spriteController.InjectDependencies(_grid, _unit, UnitMovement);
        _canvasView.InjectDependencies(UnitHealth);
        _fxController.InjectDependencies(UnitHealth);
    }

    void OnDestroy()
    {
        UnsuscribeFromEvents();
    }

    protected abstract void InitializeDependencies();

    public void Spawn()
    {
        MarkAsPooleable(false);
        UnitMovement.Teleport(_unit.CurrentCell);
        _fxController.ResetOnSpawn();
        _canvasView.ResetOnSpawn();
        OnSpawn();
        _gameObject.SetActive(true);
    }

    protected abstract void OnSpawn();
    protected abstract void HandleTurnChanged(TurnTypes.Turn turn);

    private void HandleUnitDeath(int unitId)
    {
        UnsuscribeFromEvents();
        MarkAsPooleable(true);
    }

    private void UnsuscribeFromEvents()
    {
        _turnDealer.OnTurnChanged -= HandleTurnChanged;
        UnitHealth.OnDeath -= HandleUnitDeath;
        _fxController.UnsuscribeFromEvents();
        _spriteController.UnsuscribeFromEvents();
        _canvasView.UnsuscribeFromEvents();
        UnsuscribeFromConcreteEvents();
        GameOverSignal.OnGameOver -= HandleGameOver;
        ResetGameSignal.OnGameReset -= ResetUnit;
    }

    protected virtual void UnsuscribeFromConcreteEvents()
    {
    }

    private void HandleGameOver(TurnTypes.Turn winner)
    {
        ResetUnit();
    }

    private void ResetUnit()
    {
        UnsuscribeFromEvents();
        MarkAsPooleable(true);
    }

    private void MarkAsPooleable(bool canBePooled)
    {
        CanBePooled = canBePooled;
    }
}
