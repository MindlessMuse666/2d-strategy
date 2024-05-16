using UnityEngine;

public interface IUnitConfig
{
    public GameObject UnitPrefab { get; }
    public string UnitName { get; }
    public int MoveRange { get; }
    public int Health { get; }
    public int AttackStrength { get; }
}