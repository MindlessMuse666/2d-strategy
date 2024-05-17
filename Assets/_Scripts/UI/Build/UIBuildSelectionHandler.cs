using UnityEngine;
using UnityEngine.EventSystems;

public class UIBuildSelectionHandler : MonoBehaviour, IPointerClickHandler
{
  [SerializeField] private UIBuildButtonHandler _buttonHandler;
  [SerializeField] private GameObject _structurePrefab;
  [SerializeField] private bool _isInteractable;

  public void OnPointerClick(PointerEventData eventData)
  {
    if (!_isInteractable)
    {
      _buttonHandler.ResetBuildButton();
      return;
    }

    _buttonHandler.PrepareBuildButton(_structurePrefab);
  }
}