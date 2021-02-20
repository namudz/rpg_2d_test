using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/View/TurnViewConfiguration", order = 1)]
public class TurnViewConfig : ScriptableObject
{
    public Color PlayerTurnTextColor;
    public Color CPUTurnTextColor;
}