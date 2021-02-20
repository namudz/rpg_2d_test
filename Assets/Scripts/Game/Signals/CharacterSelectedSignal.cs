public class CharacterSelectedSignal
{
    public static event System.Action<int> OnCharacterSelected;

    private static CharacterSelectedSignal _instance;
    public static CharacterSelectedSignal Instance 
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CharacterSelectedSignal();
            }
            return _instance;
        }
    }

    public void LaunchSignal(int characterInstanceId)
    {
        OnCharacterSelected?.Invoke(characterInstanceId);
    }
}