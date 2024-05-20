using UnityEngine;

public class AgentOutlineFeedback : MonoBehaviour
{
    [SerializeField] private Renderer _outlineRenderer;

    [Header("Color Settings:")]
    [SerializeField] private Color _colorToChange;
    [SerializeField] private bool _toggleOutline = true;
    [SerializeField] private bool _changeColor;

    private Color _originalColor;
    private Material _material; 

    private void Start()
    {
        _material = _outlineRenderer.material;
        _originalColor = _material.GetColor(GlobalConstants.COLOR);
    }

    public void Select() => ApplyChanges(true);
    public void Deselect() => ApplyChanges(false);

    private void ApplyChanges(bool value)
    {
        if (_toggleOutline)
             _material.SetInt(GlobalConstants.OUTLINE, value ? 1 : 0);
        if (_changeColor)
            _material.SetColor(GlobalConstants.COLOR, value ? _colorToChange : _originalColor);
    }
}