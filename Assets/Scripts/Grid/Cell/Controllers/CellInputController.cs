using UnityEngine;
using UnityEngine.EventSystems;

public class CellInputController : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _trigger;
    private Cell _cell;

    public void SetCell(Cell cell)
    {
        _cell = cell;
        if (_cell == null)
        {
            SetHandleInput(false);
        }
    }

    void OnMouseDown()
    {
        var isClickOverUi = EventSystem.current.IsPointerOverGameObject ();
        if (_cell == null || isClickOverUi) { return; }
        CellClickedSignal.Instance.LaunchSignal(_cell.Id);
    }

    public void SetHandleInput(bool toHandleInput)
    {
        _trigger.enabled = toHandleInput;
    }
}
