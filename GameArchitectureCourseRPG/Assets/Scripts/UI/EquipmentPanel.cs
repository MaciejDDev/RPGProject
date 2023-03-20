public class EquipmentPanel : ToggleablePanel
{
    public void Bind(Inventory inventory)
    {
        var panelSlots = GetComponentsInChildren<InventoryPanelSlot>();
        foreach (InventoryPanelSlot panelSlot in panelSlots)
        {
            panelSlot.Bind(inventory.GetEquipmentSlot(panelSlot.EquipmentSlotType));
        }
    }
}
