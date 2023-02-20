using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] LayerMask _layerMask;
    [SerializeField] float _rotateSpeed = 500f;
   
    GameObject _placeable;
    
    public ItemSlot _itemSlot;

    public static PlacementManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
    public void BeginPlacement(ItemSlot itemSlot)
    {
        if (itemSlot== null || itemSlot.Item == null || itemSlot.Item.PlaceablePrefab == null)
        {
            Debug.LogError("Unable to place because of null item or placeable");
            return;
        }
       

        _itemSlot = itemSlot;
        Debug.Log($"Started placing item {itemSlot.Item}");

        _placeable = Instantiate(itemSlot.Item.PlaceablePrefab);
        _placeable.transform.SetParent(transform);

    }

    void Update()
    {
        if (_placeable == null)
            return;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, float.MaxValue, _layerMask, QueryTriggerInteraction.Ignore ) )
        {
            _placeable.transform.position = hitInfo.point;
        }

        var rotation = -Input.mouseScrollDelta.y * Time.deltaTime * _rotateSpeed;
        _placeable.transform.Rotate(0f, rotation, 0f);
        
    }
}
