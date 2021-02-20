using UnityEngine;

public class DependenciesInstaller : MonoBehaviour
{

    [Header ("Game Controller")]
    [SerializeField] private GameController _gameController;
    [SerializeField] private GameInitialStateConfig _gameInitialStateConfig;

    [Header ("Grid")]
    [SerializeField] private GridConfig _gridConfig;
    [SerializeField] private GridController _gridController;

    [Header ("Camera")]
    [SerializeField] private Transform _cameraTransform;

    [Header ("Units Parent")]
    [SerializeField] private Transform _unitsParent;

    [Header ("Views")]
    [SerializeField] private GameCanvasView _gameCanvasView;
    [SerializeField] private PauseCanvasView _pauseCanvasView;
    [SerializeField] private GameOverCanvasView _gameOverCanvas;
    
    void Awake()
    {
        InjectDependencies();
    }

    private void InjectDependencies()
    {
        var turnDealer = new TurnDealer(
            new PlayerTurnHandler(),
            new CPUTurnHandler()
        );

        var gridGenerator = new GridGenerator(
            _gridConfig, 
            _gameInitialStateConfig.Characters.Length, 
            _gameInitialStateConfig.Enemies.Length
        );

        _gridController.InjectDependencies(turnDealer);

        var game = new Game(
            gridGenerator,
            _gridController, 
            new CharacterSpawner(_unitsParent, turnDealer),
            new EnemySpawner(_unitsParent, turnDealer),
            turnDealer,
            _gameInitialStateConfig,
            new CameraPositioner(_cameraTransform)
        );
        _gameCanvasView.InjectDependencies(game, turnDealer);
        _pauseCanvasView.InjectDependencies(game);
        _gameOverCanvas.InjectDependencies(game);
        _gameController.InjectDependencies(game);
    }

}
