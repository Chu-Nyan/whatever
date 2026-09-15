using System.Collections.Generic;

namespace Whatever.Entities.Items
{
    public class Inventory
    {
        public static readonly Inventory Empty = new(0);
        public const int PlayerCapacity = 80;

        public readonly ItemBase[] List;
        private readonly Dictionary<ItemBase, int> _indexByItem;

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
        }
    }
}
