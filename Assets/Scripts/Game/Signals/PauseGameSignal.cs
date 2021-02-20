public class PauseGameSignal
{
    public static event System.Action<bool> OnGamePaused;

    private static PauseGameSignal _instance;
    public static PauseGameSignal Instance 
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PauseGameSignal();
            }
            return _instance;
        }
    }

    public void LaunchSignal(bool isPaused)
    {
        OnGamePaused?.Invoke(isPaused);
    }
}