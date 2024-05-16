using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
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

        MoveUnit(endPosition);
    }

    private void MoveUnit(Vector3 endPosition)
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

        _selectedUnit.HandleMovement(direction, _moveCost);
    }
}