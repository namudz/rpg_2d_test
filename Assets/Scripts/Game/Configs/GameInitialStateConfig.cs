using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Game/GameInitialStateConfiguration", order = 1)]
public class GameInitialStateConfig : ScriptableObject
{
    [Header ("Characters")]
    public InitialUnitConfig[] Characters;

    [Header ("Enemies")]
    public InitialUnitConfig[] Enemies;
}

[Serializable]
public class InitialUnitConfig
{
    [Header ("Unit Config")]
    public UnitStatsConfig UnitConfig;

    [Header ("Spawn Config")]
    public UnitSpawnConfig SpawnConfig;
}

[Serializable]
public class UnitSpawnConfig
{
    [Tooltip ("If marked as true, will override the given Cell value")]
    public bool Randomly;
    public Vector2Int Cell;
}