using UnityEngine;

[CreateAssetMenu(fileName = "TerrainConfig", menuName = "Configs/Map/TerrainConfig")]
public class TerrainConfig : ScriptableObject
{
    [field: SerializeField] public bool IsWalkable { get; private set; }
    [field: SerializeField] public int MoveCost { get; private set; } = 10;
}