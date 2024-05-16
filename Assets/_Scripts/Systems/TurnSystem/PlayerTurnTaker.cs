using UnityEngine;

public class PlayerTurnTaker : MonoBehaviour
{
    public void WaitTurn()
    {
        foreach (ITurnDependent item in GetComponents<ITurnDependent>())
        {
            item.WaitTurn();
        }
    }
}
