namespace Whatever.Entities.Items
{
    public class QuickSlotInventory
    {
        public static readonly QuickSlotInventory Empty = new(0, 0);

        public readonly Inventory QuickSlot;
        public readonly Inventory Inventory;

        public int Count => QuickSlot.Count + Inventory.Count;

        public QuickSlotInventory(int QuickSlotCapacity, int inventoryCapacity)
        {
            QuickSlot = new(QuickSlotCapacity);
            Inventory = new(inventoryCapacity);
        }
    }
}
