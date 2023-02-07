using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    void Start() => Bind(Inventory.Instance);
    public void Bind(Inventory inventory)
    {
        var panelSlots = GetComponentsInChildren<InventoryPanelSlot>();
        for (int i = 0; i < panelSlots.Length; i++)
        {
            panelSlots[i].Bind(inventory.GeneralSlots[i]);
        }
    }
}
