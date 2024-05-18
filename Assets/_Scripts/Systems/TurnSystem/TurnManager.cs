using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    public UnityEvent  OnPlayerInputBlocked,  OnPlayerInputUnblocked;

    private Queue<EnemyTurnTaker> _enemyQueue = new();

    public void NextTurn()
    {
        Debug.Log("Waiting...");
        OnPlayerInputBlocked?.Invoke();

        EnemyTurn();
    }

    private void PlayerTurn()
    {
        foreach (PlayerTurnTaker turnTaker in FindObjectsOfType<PlayerTurnTaker>())
        {
            turnTaker.WaitTurn();

            Debug.Log($"Unit {turnTaker.name} is waiting.");
        }

        Debug.Log("New turn is ready!");

        OnPlayerInputUnblocked?.Invoke();
    }

    private void EnemyTurn()
    {
        _enemyQueue = new(FindObjectsOfType<EnemyTurnTaker>());

        StartCoroutine(EnemyTakeTurnRoutine(_enemyQueue));
    }

    private IEnumerator EnemyTakeTurnRoutine(Queue<EnemyTurnTaker> enemyQueue)
    {
        while (enemyQueue.Count > 0)
        {
            EnemyTurnTaker turnTaker = enemyQueue.Dequeue();
            turnTaker.TakeTurn();

            yield return new WaitUntil(turnTaker.IsFinished);
            
            turnTaker.Reset();
        }
        
        Debug.LogWarning("Очередь хода ИГРОКА!");
        
        PlayerTurn();
    }
}