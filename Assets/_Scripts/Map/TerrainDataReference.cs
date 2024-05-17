using UnityEngine;

public class TerrainDataReference : MonoBehaviour
{
    [SerializeField] private TerrainConfig _terrainConfig;

    public TerrainConfig Config() => _terrainConfig;
}