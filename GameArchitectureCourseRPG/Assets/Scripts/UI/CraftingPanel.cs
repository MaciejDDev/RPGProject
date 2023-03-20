using UnityEngine;

public class CraftingPanel : ToggleablePanel
{
    public void Bind(Inventory inventory)
    {
        var panelSlots = GetComponentsInChildren<InventoryPanelSlot>();
        for (int i = 0; i < panelSlots.Length; i++)
        {
            panelSlots[i].Bind(inventory.CraftingSlots[i]);
        }
    }
}
