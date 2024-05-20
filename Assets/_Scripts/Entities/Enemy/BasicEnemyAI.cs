using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour, IEnemyAI
{
    [SerializeField] private Unit _unit;
    [SerializeField] private FlashFeedback _flashFeedback;
    [SerializeField] private AgentOutlineFeedback _outlineFeedback;
    [SerializeField] private float _delayBetweenEnemySwitch = .5f;
    [SerializeField] private float _delayBetweenSteps = .3f;

    private CharacterMovement _characterMovement;

    public event Action TurnFinished;

    private void Awake() => _characterMovement = FindObjectOfType<CharacterMovement>();

    public void StartTurn()
    {
        _flashFeedback.PlayFeedback();
        _outlineFeedback.Select();

        Dictionary<Vector2Int, Vector2Int?> moveRange = _characterMovement.GetMoveRangeFor(_unit);
        List<Vector2Int> path = GetPathToRandomPosition(moveRange);
        Queue<Vector2Int> pathQueue = new(path);

        StartCoroutine(MoveUnitRoutine(pathQueue));
    }

    private List<Vector2Int> GetPathToRandomPosition(Dictionary<Vector2Int, Vector2Int?> moveRange)
    {
        List<Vector2Int> possibleDestination = moveRange.Keys.ToList();
        possibleDestination.Remove(Vector2Int.RoundToInt(transform.position));

        Vector2Int selectedDestination = possibleDestination[UnityEngine.Random.Range(0, possibleDestination.Count)];
        
        return GetPathTo(selectedDestination, moveRange);
    }

    private List<Vector2Int> GetPathTo(Vector2Int destination, Dictionary<Vector2Int, Vector2Int?> moveRange)
    {
        List<Vector2Int> path = new() { destination };

        while (moveRange[destination] != null)
        {
            path.Add(moveRange[destination].Value);
            destination = moveRange[destination].Value;
        }
        path.Reverse();

        return path.Skip(1).ToList();
    }

    private IEnumerator MoveUnitRoutine(Queue<Vector2Int> path)
    {
        yield return new WaitForSeconds(_delayBetweenEnemySwitch);
        
        if (!_unit.CanKeepMove() || path.Count <= 0)
        {
            FinishMove();
            yield break;
        }

        Vector2Int position = path.Dequeue();
        Vector3Int direction = Vector3Int.RoundToInt(
            new Vector3(position.x + GlobalConstants.CELLS_OFFSET, position.y + GlobalConstants.CELLS_OFFSET, 0) - transform.position);
        
        _unit.HandleMovement(direction, 0);

        yield return new WaitForSeconds(_delayBetweenSteps);

        if (path.Count > 0)
        {
            StartCoroutine(MoveUnitRoutine(path));
        }
        else
        {
            FinishMove();
        }
    }

    private void FinishMove()
    {
        TurnFinished?.Invoke();
        _flashFeedback.StopFeedback();
        _outlineFeedback.Deselect();
    }
}