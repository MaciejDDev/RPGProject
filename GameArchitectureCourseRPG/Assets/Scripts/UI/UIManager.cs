using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] EquipmentPanel _equipmentPanel;
    [SerializeField] StatsPanel _statsPanel;
    [SerializeField] CraftingPanel _craftingPanel;
    [SerializeField] InventoryPanel _inventoryPanel;

    public static UIManager Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }
    private void OnValidate()
    {
        _equipmentPanel = GetComponentInChildren<EquipmentPanel>();
        _statsPanel = GetComponentInChildren<StatsPanel>();
        _craftingPanel = GetComponentInChildren<CraftingPanel>();
        _inventoryPanel = GetComponentInChildren<InventoryPanel>();
    }

    internal void Bind(Player player)
    {
        _craftingPanel.Bind(player.Inventory);
        _statsPanel.Bind(player.StatsManager);
        _inventoryPanel.Bind(player.Inventory);
        _equipmentPanel.Bind(player.Inventory);
    }



}
