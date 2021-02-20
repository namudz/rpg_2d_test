using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverCanvasView : MonoBehaviour
{
    [SerializeField] private Canvas _myCanvas;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private Button _playAgainButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private ParticleSystem _victoryParticles;
    [SerializeField] private ParticleSystem _defeatParticles;
    private IGame _game;

    public void InjectDependencies(IGame game)
    {
        _game = game;
    }
    
    void Awake()
    {
        GameOverSignal.OnGameOver += ShowCanvas;
        _playAgainButton.onClick.AddListener(PlayAgain);
        _quitButton.onClick.AddListener(QuitGame);
    }

    void OnDestroy()
    {
        GameOverSignal.OnGameOver -= ShowCanvas;
    }

    private void ShowCanvas(TurnTypes.Turn winner)
    {
        var victory = winner == TurnTypes.Turn.Player;
        _titleText.SetText (victory ? "¡Victory!" : "¡Defeat!");
        _myCanvas.enabled = true;

        if (victory)
        {
            _victoryParticles.Play();
        }
        else
        {
            _defeatParticles.Play();
        }
    }

    private void PlayAgain()
    {
        _myCanvas.enabled = false;
        _victoryParticles.Stop();
        _defeatParticles.Stop();
        
        _game.ResetGame();
    }

    private void QuitGame()
    {
        _game.QuitGame();
    }
}
