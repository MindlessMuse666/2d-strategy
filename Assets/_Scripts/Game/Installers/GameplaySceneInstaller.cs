using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private GameObject _unitPrefab;

    public override void InstallBindings()
    {
        Container.BindFactory<GameObject, Unit, UnitFactory>().FromComponentInNewPrefab(_unitPrefab);
    }
}