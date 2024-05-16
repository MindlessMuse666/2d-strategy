using UnityEngine;

public class SelectionIndicatorFeedback : MonoBehaviour, ITurnDependent
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private int _defaultSortingLayer;
    private int _layerToUse;

    private void Start()
    {
        _layerToUse = SortingLayer.NameToID("SelectedObject");
        _defaultSortingLayer = _spriteRenderer.sortingLayerID;
    }

    public void Select() => ToggleSelection(true);

    public void Deselect() => ToggleSelection(false);

    public void WaitTurn() => Deselect();

    private void ToggleSelection(bool value) => 
        _spriteRenderer.sortingLayerID = value ? _layerToUse : _defaultSortingLayer;
}