using UnityEngine;

public class GameController : MonoBehaviour
{
    private IGame _game;

    public void InjectDependencies(IGame game)
    {
        _game = game;
    }

    void Start()
    {
        _game.StartNewGame();
    }
}
