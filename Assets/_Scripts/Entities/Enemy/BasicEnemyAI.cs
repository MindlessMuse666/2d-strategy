using System;
using System.Collections;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour, IEnemyAI
{
    public event Action TurnFinished;

    public void StartTurn()
    {
        Debug.LogWarning($"Очередь {gameObject.name} делать свой ход...");

        StartCoroutine(TestRoutine());
    }

    private IEnumerator TestRoutine()
    {
        yield return new WaitForSeconds(.5f);
        TurnFinished?.Invoke();

        Debug.LogWarning($"{gameObject.name} завершил свой ход!");
    }
}