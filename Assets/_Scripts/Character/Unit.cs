using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Unit : MonoBehaviour, ITurnDependent
{
    private GameObject _unitPrefab;
    private string _unitName;
    private int _moveRange;
    private int _health;
    private int _attackStrength;

    private int _currentMovePoints;

    public UnityEvent FinishedMoving;

    [Inject]
    private void Construct(UnitConfigs unitConfigs)
    {
        IUnitConfig unitConfig = ChouseUnitConfig(unitConfigs);

        _unitPrefab = unitConfig.UnitPrefab;
        _unitName = unitConfig.UnitName;
        _moveRange = unitConfig.MoveRange;
        _health = unitConfig.Health;
        _attackStrength = unitConfig.AttackStrength;
    }

    private IUnitConfig ChouseUnitConfig(UnitConfigs unitConfigs) => 
        GetComponent<Worker>() != null ? unitConfigs.FarmerConfig : unitConfigs.WarriorConfig;

    private void Start()
    {
        ResetMovePoints();
    }

    private void ResetMovePoints()
    {
        _currentMovePoints = _moveRange;
    }

    public void HandleMovement(Vector3 cardinalDirection, int moveCost)
    {
        if (_currentMovePoints - moveCost < 0)
        {
            Debug.Log($"У {_unitName} больше не осталось очков передвижения!");
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
}