using System.Collections.Generic;
using Whatever.Entities.Items;

namespace Whatever.GameSystems
{
    public class LoadoutContainer
    {
        private readonly Dictionary<string, Inventory> _inventoryByKey;

        public LoadoutContainer()
        {
            _inventoryByKey = new();
            _inventoryByKey.Add("Empty", Inventory.Empty);
        }

        public void AddInventory(string key, Inventory inven)
        {
            _inventoryByKey.Add(key, inven);
        }

        public bool TryGetInventory(string key, out Inventory inven)
        {
            return _inventoryByKey.TryGetValue(key, out inven);
        }
    }
}
