using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour, ITurnDependent
{
    [SerializeField] private UnitData _unitConfig;
    [SerializeField] private LayerMask _enemyDetectionLayer;
    [SerializeField] private AudioSource _moveSound;

    public int CurrentMovePoints { get => _currentMovePoints; }
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

        GameObject enemyUnit = GetEnemyInDirection(cardinalDirection);

        if (enemyUnit == null)
        {
            _moveSound.Play();
            transform.position += cardinalDirection;
        }
        else
            PerformAttack(enemyUnit.GetComponent<Health>());


        if (_currentMovePoints <= 0)
            FinishedMoving?.Invoke();
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

    private void PerformAttack(Health health)
    {
        health.GetHit(_unitConfig.Config.AttackStrength);

        _currentMovePoints = 0; // ПРИ АТАКЕ ОЧКИ ДВИЖЕНИЯ ЗАКАНЧИВАЮТСЯ
    }

    private GameObject GetEnemyInDirection(Vector3 cardinalDirection)
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            cardinalDirection,
            1,
            _enemyDetectionLayer);

        if (hit.collider != null)
            return hit.collider.gameObject;

        return null;
    }

    private void ResetMovePoints()
    {
        _currentMovePoints = _unitConfig.Config.MoveRange;
    }
}