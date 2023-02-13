using System;
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


    public void Bind(ItemSlot itemSlot)
    {
        _itemSlot = itemSlot;
        _itemSlot.Changed += UpdateIcon;
        UpdateIcon();
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
        if (!_itemSlot.IsEmpty && Focused != null)
            _itemSlot.Swap(Focused._itemSlot);

        _itemIcon.color = Color.white;
        _draggedItemIcon.sprite = null;
        _draggedItemIcon.enabled = false;

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

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemTooltipPanel.Instance.ShowItem(_itemSlot.Item);
    }
}