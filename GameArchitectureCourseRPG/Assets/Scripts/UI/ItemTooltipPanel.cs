using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltipPanel : MonoBehaviour
{
    [SerializeField] TMP_Text _name;
    [SerializeField] TMP_Text _description;
    [SerializeField] Image _icon;

    public static ItemTooltipPanel Instance { get; private set; }

    private void Awake() => Instance = this;
    public void ShowItem(Item item)
    {
        _name.SetText(item.name);
        _description.SetText(item.Description);
        _icon.sprite= item.Icon;
    }
}
