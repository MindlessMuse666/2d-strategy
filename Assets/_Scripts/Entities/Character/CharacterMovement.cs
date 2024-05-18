using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Map _map; // Implement Zenject
    [SerializeField] private MoveHighlight _rangeHighlight;

    private Unit _selectedUnit;
    private List<Vector2Int> _moveRange;

    public void HandleSelection(GameObject detectedObject)
    {
        if (detectedObject == null)
        {
            ResetCharacterMove();
            return;
        }

        _selectedUnit = CompareTag(GlobalConstants.PLAYER) ? detectedObject.GetComponent<Unit>() : null; 

        if (!detectedObject.TryGetComponent(out _selectedUnit))
            return;
        
        if (_selectedUnit.CanKeepMove()) { PrepareMoveRange(); }
        else { _rangeHighlight.ClearHighlight(); }

        // foreach (Vector2Int position in _moveRange)
        // {
        //     Debug.Log(position); // ОТЛАДКА ПОЗИЦИЙ ПОИСКА В ШИРИНУ
        // }
    }

    public void HandleMovement(Vector3 endPosition)
    {
        if (_selectedUnit == null) { return; }
        if (_selectedUnit.CanKeepMove() == false) { return; }

        Vector2 direction = CalculateMoveDirection(endPosition);
        Vector2Int unitTilePosition = Vector2Int.FloorToInt(
            (Vector2)_selectedUnit.transform.position + direction);

        if (_moveRange.Contains(unitTilePosition))
        {
            _selectedUnit.HandleMovement(direction, _map.GetMoveCost(unitTilePosition));

            if (_selectedUnit.CanKeepMove()) { PrepareMoveRange(); }
            else { _rangeHighlight.ClearHighlight(); }
        }
        else
            Debug.LogError($"Движение по направлению {direction} невозможно!");
    }

    private Vector2 CalculateMoveDirection(Vector3 endPosition)
    {
        Vector3 selectedUnitPosition = _selectedUnit.transform.position;
        Vector2 direction = endPosition - selectedUnitPosition;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            float sing = Mathf.Sign(direction.x);
            direction = Vector2.right * sing;
        }
        else
        {
            float sing = Mathf.Sign(direction.y);
            direction = Vector2.up * sing;
        }

        return direction;
    }

    private void ResetCharacterMove()
    {
        _rangeHighlight.ClearHighlight();
        _selectedUnit = null;
    }

    private void PrepareMoveRange()
    {
        _moveRange = _map.GetMoveRange(
                    _selectedUnit.transform.position,
                    _selectedUnit.CurrentMovePoints).Keys.ToList();

        _rangeHighlight.HighlightTiles(_moveRange);
    }
}