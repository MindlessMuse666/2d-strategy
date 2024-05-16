using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;

    private Dictionary<Vector3Int, GameObject> _buildings = new();

    public void AddStructure(Vector3 worldPosition, GameObject structure)
    {
        Vector3Int position = GetCellPositionFor(worldPosition);

        if (_buildings.ContainsKey(position))
        {
            Debug.LogError($"В позиции {worldPosition} уже есть структура \"{structure.name}!\"");

            return;
        }

        _buildings[position] = structure;
    }

    public bool IsPositionInvalid(Vector3 worldPosition) => _buildings.ContainsKey(GetCellPositionFor(worldPosition));

    private Vector3Int GetCellPositionFor(Vector3 worldPosition)
    {
        return Vector3Int.CeilToInt(_tilemap.CellToWorld(_tilemap.WorldToCell(worldPosition)));
    }
}