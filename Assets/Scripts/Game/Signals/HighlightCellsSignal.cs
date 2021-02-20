using System.Collections.Generic;

public class HighlightCellsSignal
{
    public static event System.Action<bool, List<int>, CellHighlightTypes.HighlightType> OnHighlightCells;

    private static HighlightCellsSignal _instance;
    public static HighlightCellsSignal Instance 
    {
        get
        {
            if (_instance == null)
            {
                _instance = new HighlightCellsSignal();
            }
            return _instance;
        }
    }

    public void LaunchSignal(bool toHighlight, List<int> cells, CellHighlightTypes.HighlightType highlightType)
    {
        OnHighlightCells?.Invoke(toHighlight, cells, highlightType);
    }
}