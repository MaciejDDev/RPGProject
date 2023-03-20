﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryPanelSlot : MonoBehaviour, 
    IPointerEnterHandler, 
    IPointerExitHandler, 
    IBeginDragHandler, 
    IEndDragHandler, 
    IDragHandler,
    IPointerClickHandler
{
    static InventoryPanelSlot Focused;
    
    ItemSlot _itemSlot;
    
    [SerializeField] Image _draggedItemIcon;
    [SerializeField] Image _itemIcon;
    [SerializeField] Outline _outline;
    [SerializeField] Color _draggingColor = Color.gray;
    [SerializeField] TMP_Text _stackCountText;
    [SerializeField] EquipmentSlotType _equipmentSlotType;


    public EquipmentSlotType EquipmentSlotType => _equipmentSlotType;

    private void OnValidate()
    {
        _itemIcon.sprite = _equipmentSlotType?.DefaultSprite;
        _itemIcon.enabled = _itemIcon.sprite != null;
    }
    public void Bind(ItemSlot itemSlot)
    {
        _itemSlot = itemSlot;
        _itemSlot.Changed += (Item added ,Item removed) => UpdateIconAndStackSize();
        UpdateIconAndStackSize();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Focused = this;
        _outline.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Focused == this)
            Focused = null;
        _outline.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(_itemSlot.IsEmpty) 
            return;

        _itemIcon.color = _draggingColor;
        _draggedItemIcon.sprite = _itemIcon.sprite;
        _draggedItemIcon.enabled = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _draggedItemIcon.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Focused == null && Input.GetKey(KeyCode.LeftControl))
            _itemSlot.RemoveItem();

        if (!_itemSlot.IsEmpty && Focused != null)
            Player.ActivePlayer.Inventory.Swap( _itemSlot, Focused._itemSlot);

        _itemIcon.color = Color.white;
        _draggedItemIcon.sprite = null;
        _draggedItemIcon.enabled = false;

    }

    void UpdateIconAndStackSize()
    {
        if (_itemSlot.Item != null)
        {
            _itemIcon.sprite = _itemSlot.Item.Icon;
            _itemIcon.enabled = true;
            _stackCountText.SetText(_itemSlot.StackCount.ToString());
            _stackCountText.enabled = _itemSlot.Item.MaxStackSize > 1;
        }
        else
        {
            _stackCountText.enabled = false;
            _itemIcon.sprite = _equipmentSlotType?.DefaultSprite;
            _itemIcon.enabled = _itemIcon.sprite != null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.LeftShift) && _itemSlot.IsEmpty == false)
            Player.ActivePlayer.Inventory.AddItem(_itemSlot.Item); 
        ItemTooltipPanel.Instance.ShowItem(_itemSlot);
    }
}