using UnityEngine;

[CreateAssetMenu(fileName = "WarriorConfig", menuName = "Configs/UnitConfigs/Warrior")]
public class WarriorConfig : ScriptableObject, IUnitConfig
{
    [field: SerializeField] public GameObject UnitPrefab { get; private set; }
    [field: SerializeField] public string UnitName { get; private set; } = "Warrior";
    [field: SerializeField] public int Health { get; private set; } = 2;
    [field: SerializeField] public int MoveRange { get; private set; } = 3;
    [field: SerializeField] public int AttackStrength { get; private set; } = 2;
}
