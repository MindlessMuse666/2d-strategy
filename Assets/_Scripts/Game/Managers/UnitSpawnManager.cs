using UnityEngine;

public class UnitSpawnManager : MonoBehaviour, ITurnDependent
{
    [SerializeField] private UIBuildButtonHandler _townUI;

    private Town _selectedTown = null;
    // private FarmerConfig _farmerConfig;
    // private WarriorConfig _warriorConfig;

    // [Inject]
    // private void Construct(UnitConfigs _unitConfigs)
    // {
    //     _farmerConfig = _unitConfigs.FarmerConfig;
    //     _warriorConfig = _unitConfigs.WarriorConfig;
    // }

    public void HandleSelection(GameObject selectedObject)
    {
        ResetTownBuildUI();

        if (selectedObject == null)
            return;

        if (selectedObject.TryGetComponent<Town>(out var selectedTown))
        {
            HandleTown(selectedTown);
        }
    }

    public void CreateUnit(GameObject unitToCreate)
    {
        Debug.LogWarning($"Передан юнит {unitToCreate.name}");

        if (_selectedTown.IsInProduction)
        {
            Debug.LogError("В этом городе уже есть юнит!");
            return;
        }

        _selectedTown.AddUnitToProduction(unitToCreate);
        ResetTownBuildUI();
    }

    // public void CreateUnit(GameObject unitToCreate)
    // {
    //     if (_selectedTown.IsInProduction)
    //     {
    //         Debug.LogError("В этом городе уже есть юнит!");
    //         return;
    //     }

    //     _selectedTown.AddUnitToProduction(unitToCreate);
    //     ResetTownBuildUI();
    // }

    public void WaitTurn() => ResetTownBuildUI();

    private void ResetTownBuildUI()
    {
        _selectedTown = null;
        _townUI.ToggleVisibility(false);
    }

    private void HandleTown(Town selectedTown)
    {
        _selectedTown = selectedTown;
        _townUI.ToggleVisibility(true);
    }
}