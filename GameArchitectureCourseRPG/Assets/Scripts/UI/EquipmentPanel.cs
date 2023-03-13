public class EquipmentPanel : ToggleablePanel
{
    void Start() => Bind(Inventory.Instance);
    public void Bind(Inventory inventory)
    {
        var panelSlots = GetComponentsInChildren<InventoryPanelSlot>();
        foreach (InventoryPanelSlot panelSlot in panelSlots)
        {
            panelSlot.Bind(inventory.GetEquipmentSlot(panelSlot.EquipmentSlotType));
        }
    }
}
