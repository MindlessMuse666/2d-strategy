using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    public UnityEvent  OnPlayerInputBlocked,  OnPlayerInputUnblocked;

    public void NextTurn()
    {
        Debug.Log("Waiting...");

        OnPlayerInputBlocked?.Invoke();

        foreach (PlayerTurnTaker turnTaker in FindObjectsOfType<PlayerTurnTaker>())
        {
            turnTaker.WaitTurn();

            Debug.Log($"Unit {turnTaker.name} is waiting.");
        }

        Debug.Log("New turn is ready!");

        OnPlayerInputUnblocked?.Invoke();
    }
}

public interface ITurnDependent
{
    public void WaitTurn();
}