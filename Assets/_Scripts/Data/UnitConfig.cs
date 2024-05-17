using UnityEngine;

[CreateAssetMenu(fileName = "UnitConfig", menuName = "Configs/Unit/UnitConfig")]
public class UnitConfig : ScriptableObject
{
    public string UnitName = "DefaultUnit";
    public int Health = 1;
    public int MoveRange = 2;
    public int AttackStrength = 1;
}