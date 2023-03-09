using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    [SerializeField] GameObject _previewObject;
    [SerializeField] GameObject _placedObject;
    [SerializeField] List<Renderer> _tintedRenderers;
    [SerializeField] Color _defaultColor = Color.white;
    [SerializeField] Color _invalidColor = Color.red;
    IValidatePlacement[] _validators;

    void Awake()
    {
        _validators = GetComponents<IValidatePlacement>();
        _placedObject.SetActive(false);
        _previewObject.SetActive(true);
    }

    public bool IsPlacementValid {  get; private set; }

    public void Place()
    {
        _previewObject.SetActive(false);
        _placedObject.SetActive(true);
    }

    public void SetPositionAndValidate(Vector3 point, Quaternion orientation)
    {
        transform.SetPositionAndRotation(point, orientation);
        
        IsPlacementValid = true;

        foreach (var validator in _validators)
        {
            if (validator.IsValid() == false)
            {
                IsPlacementValid = false;
                break;
            }
        }

        foreach (var renderer in _tintedRenderers)
        {
            if (renderer != null)
                renderer.material.color = IsPlacementValid ? _defaultColor : _invalidColor;
            else
                Debug.LogError("Missing renderer on placeable", gameObject);
        }
    }
}
