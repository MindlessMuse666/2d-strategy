using UnityEngine;

[CreateAssetMenu(fileName = "UnitConfigs", menuName = "Configs/UnitConfigs")]
public class UnitConfigs : ScriptableObject
{
    [field: SerializeField] public FarmerConfig FarmerConfig { get; private set; }
    [field: SerializeField] public WarriorConfig WarriorConfig { get; private set; }
}