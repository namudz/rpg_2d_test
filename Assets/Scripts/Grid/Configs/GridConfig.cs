using UnityEngine;

[CreateAssetMenu(fileName = "GridConfig", menuName = "ScriptableObjects/Grid/GridConfiguration", order = 1)]
public class GridConfig : ScriptableObject
{
    private const int MIN_SIZE = 4;
    private const int MAX_SIZE = 10;

    [Range(MIN_SIZE, MAX_SIZE)]
    [SerializeField] private int _rows = MIN_SIZE;
    [Range(MIN_SIZE, MAX_SIZE)]
    [SerializeField] private int _columns = MIN_SIZE;

    [Tooltip ("If marked as true, will override the given Cell value")]
    [SerializeField] private bool _randomSize;
    
    public float CellOffset { get{ return 1; } }

    private static bool _randomInitialized;
    private int _randomRows;
    private int _randomColumns;

    public GridConfig()
    {
        _randomInitialized = false;
        ResetGameSignal.OnGameReset += ResetRandomInitialized;
    }

    void OnDestroy()
    {
        ResetGameSignal.OnGameReset -= ResetRandomInitialized;
    }

    public int GetRows()
    {
        if (_randomSize && !_randomInitialized)
        {
            InitializeRandomSize();
        }
        return _randomSize ? _randomRows : _rows;
    }

    public int GetColumns()
    {
        if (_randomSize && !_randomInitialized)
        {
            InitializeRandomSize();
        }
        return _randomSize ? _randomColumns : _columns;
    }

    private void InitializeRandomSize()
    {
        _randomRows = Random.Range(MIN_SIZE, MAX_SIZE);
        _randomColumns = Random.Range(MIN_SIZE, MAX_SIZE);
        _randomInitialized = true;
    }

    public void ResetRandomInitialized()
    {
        _randomInitialized = false;
    }
}