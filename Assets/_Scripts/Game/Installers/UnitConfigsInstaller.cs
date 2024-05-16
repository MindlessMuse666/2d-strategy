using UnityEngine;
using Zenject;

public class UnitConfigsInstaller : MonoInstaller
{
    [SerializeField] private UnitConfigs _unitConfigs;

    public override void InstallBindings()
    {
        Container.Bind<UnitConfigs>().FromInstance(_unitConfigs).AsSingle();
    }
}