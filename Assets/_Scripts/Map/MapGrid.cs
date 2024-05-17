using System.Collections.Generic;
using UnityEngine;

public class MapGrid : MonoBehaviour
{
    public readonly static List<Vector2Int> _directionsNeighbours = new()
    {
        new Vector2Int(0, 1), // Up
        new Vector2Int(1, 0), // Right
        new Vector2Int(-1, 0), // Left
        new Vector2Int(0, -1), // Down
    };

    private Dictionary<Vector2Int, TerrainConfig> _grid = new();

    public void AddToGrid(TerrainConfig terrainConfig, List<Vector2Int> collection)
    {
        foreach (Vector2Int cell in collection)
            _grid[cell] = terrainConfig;
    }

    public List<Vector2Int> GetNeighboursFor(Vector2Int position)
    {
        List<Vector2Int> positions = new();

        foreach (Vector2Int direction in _directionsNeighbours)
        {
            Vector2Int tempPosition = position + direction;

            if (_grid.ContainsKey(tempPosition))
                positions.Add(tempPosition);
        }

        return positions;
    }

    public bool IsPositionValid(Vector2Int position) => _grid.ContainsKey(position) && _grid[position].IsWalkable;

    public int GetMoveCost(Vector2Int position) => _grid[position].MoveCost;
}