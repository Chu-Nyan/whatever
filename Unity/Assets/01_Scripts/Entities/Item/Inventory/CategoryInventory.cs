namespace Whatever.Entities.Items
{
    public class CategoryInventory
    {
        public enum Tap { Bag, Remnant, Material, Misc }

        public const int CategoryCapacity = 4;

        private readonly Inventory[] _inventories;

        public Inventory this[Tap category] => _inventories[(int)category];

        public CategoryInventory(int capacity)
        {
            _inventories = new Inventory[CategoryCapacity];
            for (int i = 0; i < _inventories.Length; i++)
            {
                _inventories[i] = new(capacity);
            }
        }
    }
}
