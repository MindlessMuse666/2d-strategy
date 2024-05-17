using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Map _map; // Implement Zenject
    [SerializeField] private int _moveCost = 1;

    private Unit _selectedUnit;

    public void HandleSelection(GameObject detectedObject)
    {
        if (detectedObject == null)
        {
            _selectedUnit = null;
            return;
        }

        _selectedUnit = detectedObject.GetComponent<Unit>();
    }

    public void HandleMovement(Vector3 endPosition)
    {
        if (_selectedUnit == null) { return; }
        if (_selectedUnit.CanKeepMove() == false) { return; }

        Vector2 direction = CalculateMoveDirection(endPosition);

        if (_map.CanMoveTo(_selectedUnit.transform.position, direction))
            _selectedUnit.HandleMovement(direction, _moveCost);
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
}