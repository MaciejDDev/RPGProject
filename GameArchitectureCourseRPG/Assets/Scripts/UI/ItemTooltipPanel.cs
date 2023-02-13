using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemTooltipPanel : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TMP_Text _name;
    [SerializeField] TMP_Text _description;
    [SerializeField] Image _icon;

    CanvasGroup _canvasGroup;
    public static ItemTooltipPanel Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowItem(Item item)
    {
        if(item == null)
        {
            Toggle(false);
        }
        else
        {
            Toggle(true);
            _name.SetText(item.name);
            _description.SetText(item.Description);
            _icon.sprite= item.Icon;
        }
    }

    void Toggle(bool visible)
    {
        _canvasGroup.alpha = visible ? 1f: 0f;
        _canvasGroup.interactable = visible;
        _canvasGroup.blocksRaycasts = visible;
    }

    public void OnPointerClick(PointerEventData eventData) => Toggle(false);
}
 