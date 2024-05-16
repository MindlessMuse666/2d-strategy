using UnityEngine;

public class Town : MonoBehaviour, ITurnDependent
{
    public bool IsInProduction { get; private set; }

    private GameObject _unitToCreate;

    public void AddUnitToProduction(GameObject unitToCreate)
    {
        IsInProduction = true;
        _unitToCreate = unitToCreate;
    }

    private void CompleteProduction()
    {
        if (!IsInProduction)
            return;

        IsInProduction = false;

        if (_unitToCreate == null)
            return;

        Instantiate(_unitToCreate, transform.position, Quaternion.identity);
    }

    public void WaitTurn()
    {
        CompleteProduction();
    }
}