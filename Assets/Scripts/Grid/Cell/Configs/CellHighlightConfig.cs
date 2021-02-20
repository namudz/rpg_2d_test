using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellHighlightConfig", menuName = "ScriptableObjects/Grid/CellHighlightConfiguration", order = 1)]
public class CellHighlightConfig : ScriptableObject
{
    [SerializeField] private Color MovementColor;
    [SerializeField] private Color AttackColor;

    private Dictionary<CellHighlightTypes.HighlightType, Color> _highlightColors;

    public Color GetColor(CellHighlightTypes.HighlightType highlightType)
    {
        InitializeDictionary();

        if(_highlightColors.ContainsKey(highlightType))
        {
            return _highlightColors[highlightType];
        }
        return Color.white;
    }

    private void InitializeDictionary()
    {
        if (_highlightColors != null) return;

        _highlightColors = new Dictionary<CellHighlightTypes.HighlightType, Color>()
        {
            {CellHighlightTypes.HighlightType.Movement, MovementColor},
            {CellHighlightTypes.HighlightType.Attack, AttackColor}
        };
    }
}