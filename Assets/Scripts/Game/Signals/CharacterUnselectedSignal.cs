public class CharacterUnselectedSignal
{
    public static event System.Action OnCharacterUnselected;

    private static CharacterUnselectedSignal _instance;
    public static CharacterUnselectedSignal Instance 
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CharacterUnselectedSignal();
            }
            return _instance;
        }
    }

    public void LaunchSignal()
    {
        OnCharacterUnselected?.Invoke();
    }
}