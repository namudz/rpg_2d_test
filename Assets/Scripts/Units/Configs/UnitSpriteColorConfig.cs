using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Units/UnitSpriteColorConfiguration", order = 1)]
public class UnitSpriteColorConfig : ScriptableObject
{
    public Color SelectedColor;
    public Color UnselectedColor;
}