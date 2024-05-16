using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HandlePlayerInput : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _threshold = .5f;

    private Collider2D[] _selectedObjects = new Collider2D[0];
    private Vector3 _startPosition;
    private int _selectedIndex;
    private bool _isFirstSelection;

    public UnityEvent<GameObject> OnHandleMouseClick;
    public UnityEvent<Vector3> OnHandleMouseFinishDragging;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
            HandleMouseClick();
        if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false)
            HandleMouseUp();
    }

    private void HandleMouseClick()
    {
        _startPosition = GetMousePosition(_camera);
        _isFirstSelection = _selectedObjects.Length == 0;

        if (_isFirstSelection) { PerformSelection(); }
    }

    private void HandleMouseUp()
    {
        Vector3 endPosition = GetMousePosition(_camera);

        if (Vector2.Distance(_startPosition, endPosition) > _threshold)
        {
            OnHandleMouseFinishDragging?.Invoke(endPosition);
            return;
        }

        if (!_isFirstSelection) { PerformSelection(); }
    }

    private Vector3 GetMousePosition(Camera camera)
    {
        Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        return mousePosition;
    }

    private void PerformSelection()
    {
        Collider2D collider = HandleMultipleObjectSelection(_startPosition);
        GameObject selectedGameObject = collider == null ? null : collider.gameObject;

        OnHandleMouseClick?.Invoke(selectedGameObject);

        if (selectedGameObject != null) { Debug.Log($"Выбран объект \"{selectedGameObject.name}\""); }
    }

    private Collider2D HandleMultipleObjectSelection(Vector3 clickPosition)
    {
        Collider2D[] tempSelectedObjects = Physics2D.OverlapPointAll(clickPosition, _layerMask);
        Collider2D selectedCollider = null;

        if (tempSelectedObjects.Length == 0)
        {
            _selectedObjects = new Collider2D[0];
        }
        else
        {
            if (CheckSameSelection(tempSelectedObjects))
            {
                _selectedIndex++;
                _selectedIndex = _selectedIndex >= _selectedObjects.Length ? 0 : _selectedIndex;
            }
            else
            {
                _selectedObjects = tempSelectedObjects;
                _selectedIndex = 0;
            }

            return _selectedObjects[_selectedIndex];
        }

        return selectedCollider;
    }

    private bool CheckSameSelection(Collider2D[] tempSelectedObjects)
    {
        if (_selectedObjects == null || _selectedObjects.Length == 0) { return false; }

        return (tempSelectedObjects.Length == _selectedObjects.Length)
            && tempSelectedObjects.Intersect(_selectedObjects).Count() == _selectedObjects.Length;
    }
}