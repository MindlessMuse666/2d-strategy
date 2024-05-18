using UnityEngine;

public class EnemyTurnTaker : MonoBehaviour
{
    private IEnemyAI _enemyAI;
    private bool _isTurnFinished;

    private void Start()
    {
        _enemyAI = GetComponent<IEnemyAI>();
        _enemyAI.TurnFinished += () => _isTurnFinished = true;
    }

    public void TakeTurn() => _enemyAI.StartTurn(); 
    public bool IsFinished() => _isTurnFinished; 
    public void Reset() => _isTurnFinished = false; 
}