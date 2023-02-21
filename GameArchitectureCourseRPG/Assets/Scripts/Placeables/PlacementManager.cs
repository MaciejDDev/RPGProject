using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] LayerMask _layerMask;
    [SerializeField] float _rotateSpeed = 500f;
    [SerializeField] List<Placeable> _allPlaceables;
   
    Placeable _placeable;
    List<PlaceableData> _placeableDatas;
    
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

        var rotation = -Input.mouseScrollDelta.y * Time.deltaTime * _rotateSpeed;
        _placeable.transform.Rotate(0f, rotation, 0f);
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, float.MaxValue, _layerMask, QueryTriggerInteraction.Ignore ) )
        {
            _placeable.SetPosition(hitInfo.point);
            if (Input.GetMouseButtonDown(0) && _placeable.IsPlacementValid)
                FinishPlacement();
        }

        
    }

    void FinishPlacement()
    {
        _placeableDatas.Add(new PlaceableData()
        {
            PlaceablePrefab = _itemSlot.Item.PlaceablePrefab.name,
            Position = _placeable.transform.position,
            Rotation = _placeable.transform.rotation

        });


        _placeable.Place();
        _placeable = null;
        _itemSlot.RemoveItem();
        _itemSlot = null;
    }

    public void Bind(List<PlaceableData> placeableDatas)
    {
        _placeableDatas = placeableDatas;

        foreach (var placeableData in _placeableDatas)
        {
            var prefab = _allPlaceables.FirstOrDefault(t => t.name == placeableData.PlaceablePrefab);
            if (prefab != null)
            {
                var placeable = Instantiate(prefab, placeableData.Position, placeableData.Rotation);
                if (placeable != null)
                    placeable.Place();
            }
            else
                Debug.LogError($"Unable respawn placeable Item {placeableData.PlaceablePrefab} because prefab was not found");
        }
    }
}
