public class PlayerTurnStateSignal
{
    public static event System.Action<TurnTypes.PlayerTurnStates> OnTurnStateUpdated;

    private static PlayerTurnStateSignal _instance;
    public static PlayerTurnStateSignal Instance 
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PlayerTurnStateSignal();
            }
            return _instance;
        }
    }

    public void LaunchSignal(TurnTypes.PlayerTurnStates turnState)
    {
        OnTurnStateUpdated?.Invoke(turnState);
    }
}