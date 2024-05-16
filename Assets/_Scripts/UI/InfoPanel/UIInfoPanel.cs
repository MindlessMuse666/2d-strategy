using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInfoPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _infoImage;

    public void SetData(Sprite sprite, string text)
    {
        _infoImage.sprite = sprite;
        _nameText.text = text;
    }

    public void ToggleVisibility(bool value) => gameObject.SetActive(value);
}