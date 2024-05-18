using System;

public interface IEnemyAI
{
    public event Action TurnFinished;

    public void StartTurn();
}