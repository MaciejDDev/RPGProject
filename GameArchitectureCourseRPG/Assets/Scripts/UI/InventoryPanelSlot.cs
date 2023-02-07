using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryPanelSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    ItemSlot _itemSlot;
    [SerializeField] Image _itemIcon;
    [SerializeField] Outline _outline;

    public void Bind(ItemSlot itemSlot)
    {
        _itemSlot = itemSlot;
        _itemSlot.Changed += UpdateIcon;
        UpdateIcon();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _outline.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _outline.enabled = false;
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