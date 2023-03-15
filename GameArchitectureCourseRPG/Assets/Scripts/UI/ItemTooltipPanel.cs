using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemTooltipPanel : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TMP_Text _name;
    [SerializeField] TMP_Text _description;
    [SerializeField] TMP_Text _stats;
    [SerializeField] Image _icon;
    [SerializeField] Button _placeButton;

    CanvasGroup _canvasGroup;
    ItemSlot _itemSlot;

    public static ItemTooltipPanel Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
        _canvasGroup = GetComponent<CanvasGroup>();
        Toggle(false);
        _placeButton.onClick.AddListener(TryPlace);
    }

    private void TryPlace()
    {
        PlacementManager.Instance.BeginPlacement(_itemSlot);
        Toggle(false);
    }

    public void ShowItem(ItemSlot itemSlot)
    {
        _itemSlot = itemSlot;

        var item = itemSlot.Item;
        if(item == null)
        {
            Toggle(false);
        }
        else
        {
            Toggle(true);
            _name.SetText(item.name);
            _description.SetText(item.Description);
            _stats.SetText(GetStatsText(item));
            _icon.sprite= item.Icon;
            _placeButton.gameObject.SetActive(item.PlaceablePrefab != null);
        }
    }

    string GetStatsText(Item item)
    {
        StringBuilder b = new StringBuilder();
        foreach (var statMod in item.StatMods)
        {
            if ( statMod.Value > 0)
                b.AppendLine($"+{statMod.Value} {statMod.StatType.name}");
            else
                b.AppendLine($"-{statMod.Value} {statMod.StatType.name}");
        }
        return b.ToString();
    }

    void Toggle(bool visible)
    {
        _canvasGroup.alpha = visible ? 1f: 0f;
        _canvasGroup.interactable = visible;
        _canvasGroup.blocksRaycasts = visible;
    }

    public void OnPointerClick(PointerEventData eventData) => Toggle(false);
}
 