using UnityEngine;

public class InfoManager : MonoBehaviour, ITurnDependent
{
    [SerializeField] private UIInfoPanel _infoPanel;

    private void Start()
    {
        HideInfoPanel();
    }

    public void HandleSelection(GameObject selectedObject)
    {
        HideInfoPanel();

        if (selectedObject == null)
            return;

        InfoProvider infoProvider = selectedObject.GetComponent<InfoProvider>();

        if (infoProvider == null)
            return;

        ShowInfoPanel(infoProvider);
    }

    public void WaitTurn()
    {
        HideInfoPanel();
    }

    public void HideInfoPanel()
    {
        _infoPanel.ToggleVisibility(false);
    }

    private void ShowInfoPanel(InfoProvider infoProvider)
    {
        _infoPanel.ToggleVisibility(true);
        _infoPanel.SetData(infoProvider.Sprite, infoProvider.DisplayName);
    }
}