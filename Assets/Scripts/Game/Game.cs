
public class Game : IGame
{
    private readonly IGridGenerator _gridGenerator;
    private readonly GridController _gridController;
    private readonly IUnitSpawner _charactersSpawner;
    private readonly IUnitSpawner _enemiesSpawner;
    private readonly ITurnDealer _turnDealer;
    private readonly GameInitialStateConfig _initialStateConfig;
    private readonly ICameraPositioner _cameraPositioner;
    private bool _isPaused;

    public Game(
        IGridGenerator gridGenerator,
        GridController gridController,
        IUnitSpawner charactersSpawner,
        IUnitSpawner enemiesSpawner,
        ITurnDealer turnDealer,
        GameInitialStateConfig initialStateConfig,
        ICameraPositioner cameraPositioner)
    {
        _gridGenerator = gridGenerator;
        _gridController = gridController;
        _charactersSpawner = charactersSpawner;
        _enemiesSpawner = enemiesSpawner;
        _turnDealer = turnDealer;
        _initialStateConfig = initialStateConfig;
        _cameraPositioner = cameraPositioner;
    }

    public void StartNewGame()
    {
        _isPaused = false;
        
        var gameGrid = _gridGenerator.GenerateGrid();
        _gridController.Render(gameGrid);
        _cameraPositioner.CenterCamera(gameGrid.Rows, gameGrid.Columns);

        var characters = _charactersSpawner.SpawnUnits(gameGrid, _initialStateConfig.Characters);
        _turnDealer.SetCharacterUnits(characters);
        var enemies = _enemiesSpawner.SpawnUnits(gameGrid, _initialStateConfig.Enemies);
        _turnDealer.SetEnemyUnits(enemies);
        
        _turnDealer.SetCurrentTurn(TurnTypes.Turn.Player);
    }

    public void ResetGame()
    {
        ResetGameSignal.Instance.LaunchSignal();
        StartNewGame();
    }

    public void PauseGame()
    {
        _isPaused = !_isPaused;
        PauseGameSignal.Instance.LaunchSignal(_isPaused);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			UnityEngine.Application.Quit();
		#endif
    }
}