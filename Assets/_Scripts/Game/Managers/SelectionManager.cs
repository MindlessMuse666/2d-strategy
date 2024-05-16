using UnityEngine;

public class SelectionManager : MonoBehaviour, ITurnDependent
{
    private FlashFeedback _flashFeedback;
    private SelectionIndicatorFeedback _selectionFeedback;

    public void HandleSelection(GameObject detectedCollider)
    {
        DeselectOldObject();

        if (detectedCollider == null)
            return;

        if (detectedCollider.TryGetComponent(out _selectionFeedback))
            _selectionFeedback.Select();

        if (detectedCollider.TryGetComponent<Unit>(out var unit))
        {
            if (unit.CanKeepMove() == false)
                return;
        }

        if (detectedCollider.TryGetComponent(out _flashFeedback))
            _flashFeedback.PlayFeedback();
    }

    public void WaitTurn()
    {
        DeselectOldObject();
    }

    private void DeselectOldObject()
    {
        if (_flashFeedback != null)
        {
            _flashFeedback.StopFeedback();
            _flashFeedback = null;
        }

        if (_selectionFeedback != null)
        {
            _selectionFeedback.Deselect();
            _selectionFeedback = null;
        }
    }
}