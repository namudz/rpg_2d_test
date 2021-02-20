using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Units/UnitConfiguration", order = 1)]
public class UnitStatsConfig : ScriptableObject
{
    [Header ("Prefab")]
    public GameObject Prefab;
    
    [Header ("Unit Stats")]
    [Min(1f)]
    public float HealthPoints = 1f;
    [Range(1, 9)]
    public int MovementRange = 1;
    [Range(1, 9)]
    public int AttackRange = 1;
    [Min(1f)]
    public float AttackPoints = 1f;
}