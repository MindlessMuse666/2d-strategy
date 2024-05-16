using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIBuildButtonHandler : MonoBehaviour
{
    [SerializeField] private Button _buildButton;
    [SerializeField] private UnityEvent<GameObject> OnBuildButtonClick;

    private GameObject _structurePrefab;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void PrepareBuildButton(GameObject structurePrefab)
    {
        _structurePrefab = structurePrefab;
        _buildButton.gameObject.SetActive(true);
    }

    public void ResetBuildButton()
    {
        _structurePrefab = null;
        _buildButton.gameObject.SetActive(false);
    }

    public void ToggleVisibility(bool value)
    {
        gameObject.SetActive(value);

        if (value)
        {
            ResetBuildButton();
        }
    }

    public void HandleButtonClick() => OnBuildButtonClick?.Invoke(_structurePrefab);
}