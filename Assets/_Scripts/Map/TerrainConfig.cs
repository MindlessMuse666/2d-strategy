using UnityEngine;

[CreateAssetMenu(fileName = "TerrainConfig", menuName = "Configs/Map/TerrainConfig")]
public class TerrainConfig : ScriptableObject
{
    public bool IsWalkable;
    public int MoveCost = 10;
}