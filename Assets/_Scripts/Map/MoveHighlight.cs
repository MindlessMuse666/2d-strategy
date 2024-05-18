using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveHighlight : MonoBehaviour
{
    [SerializeField] private Tilemap _highlightTilemap;
    [SerializeField] private TileBase _highlightTile;

    public void ClearHighlight() => _highlightTilemap.ClearAllTiles();

    public void HighlightTiles(IEnumerable<Vector2Int> cellPositions)
    {
        ClearHighlight();

        foreach (Vector2Int tilePosition in cellPositions)
        {
            _highlightTilemap.SetTile((Vector3Int)tilePosition, _highlightTile);            
        }
    }
}