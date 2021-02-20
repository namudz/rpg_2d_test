using UnityEngine;

public class UnitSpriteController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _unitSprite;
    [SerializeField] private UnitSpriteColorConfig _colorsConfig;

    private int _gridTotalCells;
    private Unit _unit;
    private IUnitMovement _unitMovement;

    public void InjectDependencies(IGrid grid, Unit unit, IUnitMovement unitMovement)
    {
        _gridTotalCells = grid.Rows * grid.Columns;
        _unit = unit;
        _unitMovement = unitMovement;
        _unitMovement.OnMovementFinished += UpdateSortingOrder;
    }

    public void UnsuscribeFromEvents()
    {
        _unitMovement.OnMovementFinished -= UpdateSortingOrder;
    }

    public void SetIsSelectable(bool isSelectable)
    {
        _unitSprite.color = isSelectable ? _colorsConfig.UnselectedColor : _colorsConfig.SelectedColor;
    }

    private void UpdateSortingOrder()
    {
        _unitSprite.sortingOrder = _gridTotalCells - _unit.CurrentCell.Id;
    }
}