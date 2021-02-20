using System.Collections.Generic;
using UnityEngine;

public class CellHighlightController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Animator _animator;
    [SerializeField] private CellHighlightConfig _highlightConfig;
    private int _cellId;

    public void SetCell(Cell cell)
    {
        if (cell != null)
        {
            _cellId = cell.Id;
            HighlightCellsSignal.OnHighlightCells -= Highlight;
            HighlightCellsSignal.OnHighlightCells += Highlight;
        }
        else
        {
            HighlightCellsSignal.OnHighlightCells -= Highlight;
        }
    }

    void OnDestroy()
    {
        HighlightCellsSignal.OnHighlightCells -= Highlight;
    }

    private void Highlight(bool toHighlight, List<int> cellsIdsToHighlight, CellHighlightTypes.HighlightType highlightType)
    {
        if (!cellsIdsToHighlight.Contains(_cellId)){ return; }
        

        _sprite.enabled = toHighlight;
        if (!_animator.enabled && toHighlight)
        {
            _animator.Rebind();
        }
        _animator.enabled = toHighlight;

        if (toHighlight)
        {
            _sprite.color = _highlightConfig.GetColor(highlightType);
        }
    }
}
