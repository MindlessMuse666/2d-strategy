using UnityEngine;

public class BuildManager : MonoBehaviour, ITurnDependent
{
    [SerializeField] private Map _map;
    [SerializeField] private UIBuildButtonHandler _unitBuildUI;
    [SerializeField] private InfoManager _infoManager;
    [SerializeField] private AudioSource _buildUpAudio;

    private Unit _farmerUnit;

    public void HandleSelection(GameObject selectedObject)
    {
        ResetBuildSystem();

        if (selectedObject == null)
            return;

        if (selectedObject.TryGetComponent<Worker>(out var worker))
        {
            HandleUnitSelection(worker);
        }
    }

    public void BuildStructure(GameObject structurePrefab)
    {
        Debug.LogWarning($"Передана структура {structurePrefab.name}");
        
        if (_map.IsPositionInvalid(_farmerUnit.transform.position))
        {
            Debug.LogWarning($"В этой позиции уже есть структура!");

            return;
        }

        Debug.Log($"Placing structure at {_farmerUnit.transform.position}!");

        GameObject structure = Instantiate(structurePrefab, _farmerUnit.transform.position, Quaternion.identity);

        _map.AddStructure(_farmerUnit.transform.position, structure);

        _buildUpAudio.Play();

        if (structurePrefab.name == "TownStructure")
        {
            _farmerUnit.DestroyUnit();
            _infoManager.HideInfoPanel();
        }
        else
        {
            _farmerUnit.FinishMove();
        }

        ResetBuildSystem();
    }

    public void WaitTurn()
    {
        ResetBuildSystem();
    }

    private void HandleUnitSelection(Worker worker)
    {
        _farmerUnit = worker.GetComponent<Unit>();

        if (_farmerUnit != null && _farmerUnit.CanKeepMove())
        {
            _unitBuildUI.ToggleVisibility(true);
            _farmerUnit.FinishedMoving.AddListener(ResetBuildSystem);
        }
    }

    private void ResetBuildSystem()
    {
        if (_farmerUnit != null)
            _farmerUnit.FinishedMoving.RemoveListener(ResetBuildSystem);
        
        _farmerUnit = null;
        _unitBuildUI.ToggleVisibility(false);
    }
}