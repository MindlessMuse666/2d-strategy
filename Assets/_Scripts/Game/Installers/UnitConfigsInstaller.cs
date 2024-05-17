using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "UnitConfigsInstaller", menuName = "Installers/Configs/UnitConfigsInstaller")]
public class UnitConfigsInstaller : ScriptableObjectInstaller<UnitConfigsInstaller>
{
    [SerializeField] private UnitConfigs _unitConfigs;

    public override void InstallBindings()
    {
        Container.Bind<UnitConfigs>().FromInstance(_unitConfigs).AsSingle();
    }
}