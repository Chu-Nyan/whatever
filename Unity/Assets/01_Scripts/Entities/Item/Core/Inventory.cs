using System.Collections.Generic;

namespace Whatever.Entities.Items
{
    public class CategoryInventory
    {
        public const int CategoryCapacity = 4;
        private readonly Inventory[] _inventories;

        public Inventory this[ItemCategory category] => _inventories[(int)category];

        public CategoryInventory(int capacity)
        {
            _inventories = new Inventory[CategoryCapacity];
            for (int i = 0; i < _inventories.Length; i++)
            {
                _inventories[i] = new(capacity);
            }
        }
    }

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

    public class Inventory
    {
        public const int PlayerCapacity = 80;

        public readonly ItemBase[] List;
        private readonly Dictionary<ItemBase, int> _indexByItem;
        private int _count;

        public int Count => _count;

        public Inventory(int capacity)
        {
            List = new ItemBase[capacity];
            _indexByItem = new(capacity);
        }

        public bool TryAdd(ItemBase item)
        {
            for (int i = 0; i < List.Length; i++)
            {
                if (List[i] != null)
                    continue;

                List[i] = item;
                _indexByItem.Add(item, i);
                _count++;
                return true;
            }

            return false;
        }

        public bool TryGetItem(int itemID, out ItemBase item)
        {
            for (int i = List.Length - 1; i >= 0; i--)
            {
                if (List[i] == null)
                    continue;
                if (List[i].Def.ID != itemID)
                    continue;

                item = List[i];
                return true;
            }

            item = null;
            return false;
        }

        public void Refresh(ItemBase item)
        {
            if (item.CanRemove() == false)
                return;

            Remove(item);
        }

        public void Remove(ItemBase item)
        {
            if (_indexByItem.ContainsKey(item) == false)
                return;

            int index = _indexByItem[item];
            List[index] = null;
            _indexByItem.Remove(item);
            _count--;
        }
    }
}
