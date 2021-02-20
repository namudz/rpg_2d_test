public class EnemyUnitController : AUnitController 
{
    protected override void OnSpawn()
    {
        _grid.AddEnemy(this);
    }
    
    protected override void HandleTurnChanged(TurnTypes.Turn turn)
    {
    }

    protected override void InitializeDependencies()
    {
    }
}