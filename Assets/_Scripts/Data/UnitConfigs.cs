using UnityEngine;

[CreateAssetMenu(fileName = "UnitConfigs", menuName = "Configs/UnitConfigs")]
public class UnitConfigs : ScriptableObject
{
    [SerializeField] private UnitConfig FarmerConfig;
    [SerializeField] private UnitConfig WarriorConfig;
}