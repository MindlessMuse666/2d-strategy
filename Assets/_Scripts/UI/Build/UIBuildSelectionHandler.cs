using UnityEngine;
using UnityEngine.EventSystems;

public class UIBuildSelectionHandler : MonoBehaviour, IPointerClickHandler
{
  [SerializeField] private GameObject _structurePrefab;
  [SerializeField] private bool _isInteractable = false;

  private UIBuildButtonHandler _buttonHandler;

  private void Awake()
  {
    _buttonHandler = GetComponentInParent<UIBuildButtonHandler>();
  }

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