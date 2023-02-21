using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    [SerializeField] GameObject _previewObject;
    [SerializeField] GameObject _placedObject;
    [SerializeField] List<Renderer> _tintedRenderers;
    [SerializeField] Color _defaultColor;
    [SerializeField] Color _invalidColor;

    public bool IsPlacementValid {  get; private set; }

    public void Place()
    {
        _previewObject.SetActive(false);
        _placedObject.SetActive(true);
    }

    public void SetPosition(Vector3 point)
    {
        transform.position = point;
        IsPlacementValid = true;

        if (Vector3.Distance(transform.position, FindObjectOfType<ThirdPersonMover>().transform.position) > 10f)
        {
            IsPlacementValid = false;
        }

        foreach (var renderer in _tintedRenderers)
        {
            renderer.material.color = IsPlacementValid ? _defaultColor : _invalidColor;
        }
    }
}
