using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour, ITurnDependent
{
    [SerializeField] private UnitData _unitConfig;
    
    private int _currentMovePoints;

    public UnityEvent FinishedMoving;

    private void Start()
    {
        ResetMovePoints();
    }

    public void HandleMovement(Vector3 cardinalDirection, int moveCost)
    {
        if (_currentMovePoints - moveCost < 0)
        {
            Debug.Log($"У {_unitConfig.Config.UnitName} больше не осталось очков передвижения!");
            return;
        }

        _currentMovePoints -= moveCost;

        if (_currentMovePoints <= 0)
            FinishedMoving?.Invoke();

        transform.position += cardinalDirection;
    }

    public bool CanKeepMove() => _currentMovePoints > 0;

    public void WaitTurn()
    {
        ResetMovePoints();
    }

    public void DestroyUnit()
    {
        FinishedMoving?.Invoke();

        Destroy(gameObject);
    }

    public void FinishMove()
    {
        FinishedMoving?.Invoke();

        _currentMovePoints = 0;
    }

    private void ResetMovePoints()
    {
        _currentMovePoints = _unitConfig.Config.MoveRange;
    }
}