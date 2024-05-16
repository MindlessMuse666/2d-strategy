using UnityEngine;

public class UnitData : MonoBehaviour
{
    [SerializeField] private IUnitConfig _config;

    public IUnitConfig Config { get { return _config; } }
}