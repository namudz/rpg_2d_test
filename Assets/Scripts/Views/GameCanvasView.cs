using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasView : MonoBehaviour
{
    [Header ("Turn")]
    [SerializeField] private TextMeshProUGUI _turnValueText;
    [SerializeField] private TurnViewConfig _viewConfig;
    [Header ("Pause")]
    [SerializeField] private Button _pauseButton;

    [Header ("Skip turn")]
    [SerializeField] private Button _skipTurnButton;
    private IGame _game;
    private ITurnDealer _turnDealer;

    public void InjectDependencies(IGame game, ITurnDealer turnDealer)
    {
        _game = game;
        _turnDealer = turnDealer;
        _turnDealer.OnTurnChanged += OnTurnChanged;
    }

    void Awake()
    {
        _skipTurnButton.onClick.AddListener(SkipTurn);
        _pauseButton.onClick.AddListener(PauseGame);
    }

    private void SkipTurn()
    {
        _turnDealer.SetCurrentTurn(TurnTypes.Turn.CPU);
    }

    private void PauseGame()
    {
        _game.PauseGame();
    }

    private void OnTurnChanged(TurnTypes.Turn turn)
    {
        var isPlayerTurn = turn == TurnTypes.Turn.Player;

        _turnValueText.SetText(turn.ToString());
        _turnValueText.color = isPlayerTurn ? _viewConfig.PlayerTurnTextColor : _viewConfig.CPUTurnTextColor;

        _skipTurnButton.interactable = isPlayerTurn;
    }
}