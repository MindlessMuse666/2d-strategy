using UnityEngine;

public class UnitData : MonoBehaviour
{
    [SerializeField] private UnitConfig _unitConfig;

    public UnitConfig Config { get { return _unitConfig; } }
}