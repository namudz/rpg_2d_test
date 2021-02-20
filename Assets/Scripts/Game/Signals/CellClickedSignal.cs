public class CellClickedSignal
{
    public static event System.Action<int> OnCellClicked;

    private static CellClickedSignal _instance;
    public static CellClickedSignal Instance 
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CellClickedSignal();
            }
            return _instance;
        }
    }

    public void LaunchSignal(int cellId)
    {
        OnCellClicked?.Invoke(cellId);
    }
}