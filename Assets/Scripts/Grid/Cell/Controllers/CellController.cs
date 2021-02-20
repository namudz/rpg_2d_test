using UnityEngine;

public class CellController : MonoBehaviour
{
    [SerializeField] private CellRenderer _renderer;
    [SerializeField] private CellInputController _inputController;
    [SerializeField] private CellHighlightController _highlightController;

    public void SetUp(Cell cell)
    {
        _inputController.SetCell(cell);
        _highlightController.SetCell(cell);
        _renderer.Render(cell != null);
    }

    public void SetHandleInput(bool toHandleInput)
    {
        _inputController.SetHandleInput(toHandleInput);
    }
}