using UnityEngine;

public class UnitSpawnManager : MonoBehaviour, ITurnDependent
{
    [SerializeField] private UIBuildButtonHandler _townUI;

    private Town _selectedTown = null;

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
        if (_selectedTown.IsInProduction)
        {
            Debug.LogError("В этом городе уже есть юнит!");
            return;
        }

        _selectedTown.AddUnitToProduction(unitToCreate);
        ResetTownBuildUI();
    }

    public void WaitTurn() => ResetTownBuildUI();

    private void ResetTownBuildUI() => _townUI.ToggleVisibility(false);

    private void HandleTown(Town selectedTown) => _townUI.ToggleVisibility(true);
}