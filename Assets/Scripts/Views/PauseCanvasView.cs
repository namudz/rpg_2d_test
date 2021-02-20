using UnityEngine;
using UnityEngine.UI;

public class PauseCanvasView : MonoBehaviour
{
    [SerializeField] private Canvas _myCanvas;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _resumeBackgroundButton;
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _quitButton;

    private IGame _game;

    public void InjectDependencies(IGame game)
    {
        _game = game;
        PauseGameSignal.OnGamePaused += Show;
    }

    void Awake()
    {
        _newGameButton.onClick.AddListener(ResetGame);
        _quitButton.onClick.AddListener(QuitGame);
        _resumeButton.onClick.AddListener(ResumeGame);
        _resumeBackgroundButton.onClick.AddListener(ResumeGame);
    }

    private void Show(bool isGamePaused)
    {
        _myCanvas.enabled = isGamePaused;
    }

    private void ResumeGame()
    {
        _game.PauseGame();
    }

    private void ResetGame()
    {
        Show(false);
        _game.ResetGame();
    }

    private void QuitGame()
    {
        _game.QuitGame();
    }
}