using System.Collections.Generic;

namespace Whatever.Entities.Items
{
    public class Inventory
    {
        public const int PlayerCapacity = 80;

        private readonly ItemBase[] _list;
        private readonly Dictionary<ItemBase, int> _indexByItem;
        private int _count;

        public ItemBase[] List => _list;
        public int Count => _count;

        public ItemBase this[int slotIndex] => _list[slotIndex];

        public Inventory(int capacity)
        {
            _list = new ItemBase[capacity];
            _indexByItem = new(capacity);
        }

        public bool TryAdd(ItemBase item)
        {
            for (int i = 0; i < _list.Length; i++)
            {
                if (_list[i] != null)
                    continue;

                _list[i] = item;
                _indexByItem.Add(item, i);
                _count++;
                return true;
            }

            return false;
        }

        public bool TryGetItem(int itemID, out ItemBase item)
        {
            for (int i = _list.Length - 1; i >= 0; i--)
            {
                if (_list[i] == null)
                    continue;
                if (_list[i].Def.ID != itemID)
                    continue;

                item = _list[i];
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
            _list[index] = null;
            _indexByItem.Remove(item);
            _count--;
        }
    }
}
