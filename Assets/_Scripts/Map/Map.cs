using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    [SerializeField] private Tilemap _islandCollidersTilemap, _seaTilemap, _forestTilemap, _mountainsTilemap;
    [SerializeField] private bool _showEmpty, _showMountains, _showForest;

    private Dictionary<Vector3Int, GameObject> _buildings = new();
    private List<Vector2Int> _islandTiles, _forestTiles, _mountainTiles, _emptyTiles;
    private MapGrid _mapGrid;

    private void Awake()
    {
        _forestTiles = GetTilemapWorldPositionsFrom(_forestTilemap);
        _mountainTiles = GetTilemapWorldPositionsFrom(_mountainsTilemap);
        _islandTiles = GetTilemapWorldPositionsFrom(_islandCollidersTilemap);
        _emptyTiles = GetEmptyTiles(_islandTiles, _forestTiles.Concat(_mountainTiles).ToList());

        PrepareMapGrid();
    }

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

    public bool CanMoveTo(Vector2 unitPosition, Vector2 direction)
    {
        Vector2Int unitTilePosition = Vector2Int.FloorToInt(unitPosition + direction);
        List<Vector2Int> neighbours = _mapGrid.GetNeighboursFor(Vector2Int.FloorToInt(unitPosition));

        foreach (Vector2Int cellPosition in neighbours) // ОТЛАДКА СОСЕДЕЙ КЛЕТКИ
        {
            Debug.Log(cellPosition);
        }

        return neighbours.Contains(unitTilePosition) && _mapGrid.IsPositionValid(unitTilePosition);
    }

    public bool IsPositionInvalid(Vector3 worldPosition) => _buildings.ContainsKey(GetCellPositionFor(worldPosition));

    private List<Vector2Int> GetTilemapWorldPositionsFrom(Tilemap tilemap)
    {
        List<Vector2Int> tempList = new();
        
        foreach (Vector2Int cellPosition in tilemap.cellBounds.allPositionsWithin)
        {
            if (!tilemap.HasTile((Vector3Int)cellPosition))
                continue;
            
            Vector3Int worldPosition = GetWorldPositionFor(cellPosition);
            tempList.Add((Vector2Int)worldPosition);
        }

        return tempList;
    }

    private List<Vector2Int> GetEmptyTiles(List<Vector2Int> islandTiles, List<Vector2Int> nonEmptyTiles)
    {
        HashSet<Vector2Int> emptyTilesHashset = new(islandTiles);

        emptyTilesHashset.ExceptWith(nonEmptyTiles);

        return new List<Vector2Int>(emptyTilesHashset);
    }

    private Vector3Int GetCellPositionFor(Vector3 worldPosition) =>
        Vector3Int.CeilToInt(_islandCollidersTilemap.CellToWorld(_islandCollidersTilemap.WorldToCell(worldPosition)));

    private Vector3Int GetWorldPositionFor(Vector2Int cellPosition) =>
        Vector3Int.CeilToInt(_islandCollidersTilemap.CellToWorld((Vector3Int)cellPosition));

    private void PrepareMapGrid()
    {
        _mapGrid = new();

        _mapGrid.AddToGrid(
            _forestTilemap.GetComponent<TerrainDataReference>().Config(),
            _forestTiles);
        _mapGrid.AddToGrid(
            _mountainsTilemap.GetComponent<TerrainDataReference>().Config(),
            _mountainTiles);
        _mapGrid.AddToGrid(
            _islandCollidersTilemap.GetComponent<TerrainDataReference>().Config(),
            _emptyTiles);
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;
        
        DrawGizmoOf(_emptyTiles, Color.white, _showEmpty);
        DrawGizmoOf(_forestTiles, Color.yellow, _showForest);
        DrawGizmoOf(_mountainTiles, Color.red, _showMountains);
    }

    private void DrawGizmoOf(List<Vector2Int> tiles, Color gizmoColor, bool isShowing)
    {
        if (isShowing)
        {
            Gizmos.color = gizmoColor;

            foreach (Vector2Int position in tiles)
                Gizmos.DrawSphere(new Vector3(position.x + .5f, position.y + .5f, 0), .3f);
        }
    }
}