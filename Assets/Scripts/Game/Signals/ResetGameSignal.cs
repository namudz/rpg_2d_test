public class ResetGameSignal
{
    public static event System.Action OnGameReset;

    private static ResetGameSignal _instance;
    public static ResetGameSignal Instance 
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ResetGameSignal();
            }
            return _instance;
        }
    }

    public void LaunchSignal()
    {
        OnGameReset?.Invoke();
    }
}