using TMPro;
using UnityEngine;

public class UIResource : MonoBehaviour
{
    [SerializeField] private TMP_Text _resourceAmount;
    [SerializeField] private ResourceType _resourceType;

    public ResourceType ResourceType => _resourceType;

    private void Start()
    {
        if (_resourceType == ResourceType.None)
            throw new System.ArgumentException($"Resorce type can't be \"None\" in {gameObject.name}!");
    }

    public void SetValue(int value) => _resourceAmount.text = value.ToString();
}

public enum ResourceType
{
    None, Wood, Food, Gold
}