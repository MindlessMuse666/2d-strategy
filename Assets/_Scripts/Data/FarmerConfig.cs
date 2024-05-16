using UnityEngine;

[CreateAssetMenu(fileName = "FarmerConfig", menuName = "Configs/UnitConfigs/Farmer")]
public class FarmerConfig : ScriptableObject, IUnitConfig
{
    [field: SerializeField] public GameObject UnitPrefab { get; private set; }
    [field: SerializeField] public string UnitName { get; private set; } = "Farmer";
    [field: SerializeField] public int Health { get; private set; } = 1;
    [field: SerializeField] public int MoveRange { get; private set; } = 2;
    [field: SerializeField] public int AttackStrength { get; private set; } = 1;
}