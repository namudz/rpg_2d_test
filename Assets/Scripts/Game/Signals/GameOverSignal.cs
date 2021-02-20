public class GameOverSignal
{
    public static event System.Action<TurnTypes.Turn> OnGameOver;

    private static GameOverSignal _instance;
    public static GameOverSignal Instance 
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameOverSignal();
            }
            return _instance;
        }
    }

    public void LaunchSignal(TurnTypes.Turn turn)
    {
        OnGameOver?.Invoke(turn);
    }
}