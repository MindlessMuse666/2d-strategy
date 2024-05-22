using System;
using System.Collections.Generic;
using UnityEngine;

public class UIResourceManager : MonoBehaviour
{
    private Dictionary<ResourceType, UIResource> _resourceUiDictionary = new();

    private void Awake()
    {
        PrepareResourceDictionary();
    }

    private void PrepareResourceDictionary()
    {
        foreach (UIResource uiResourceReference in GetComponentsInChildren<UIResource>())
        {
            if (_resourceUiDictionary.ContainsKey(uiResourceReference.ResourceType))
                throw new ArgumentException($"Dictionary already contains a {uiResourceReference.ResourceType}");

            _resourceUiDictionary[uiResourceReference.ResourceType] = uiResourceReference;
            SetResource(uiResourceReference.ResourceType, 0);
        }
    }

    private void SetResource(ResourceType resourceType, int value)
    {
        try
        {
            _resourceUiDictionary[resourceType].SetValue(value);
        }
        catch (Exception)
        {
            throw new Exception($"Dictionary does not contain a UiReference for {resourceType}");
        }        
        
    }
}