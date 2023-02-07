using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelSlot : MonoBehaviour
{
    ItemSlot _itemSlot;
    [SerializeField] Image _itemIcon;

    public void Bind(ItemSlot itemSlot)
    {
        _itemSlot = itemSlot;
        _itemSlot.Changed += UpdateIcon;
        UpdateIcon();
    }

    private void UpdateIcon()
    {
        if (_itemSlot.Item != null)
        {
            _itemIcon.sprite = _itemSlot.Item.Icon;
            _itemIcon.enabled = true;
        }
        else
        {
            _itemIcon.sprite = null;
            _itemIcon.enabled = false;
        }
    }
}