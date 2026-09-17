using System.Collections.Generic;
using UnityEngine;
using Whatever.Entities.Items;

namespace Whatever.GameSystems
{
    public class PersistentDataContainer
    {
        private readonly Dictionary<string, Inventory> _inventoryByKey;

        public PersistentDataContainer()
        {
            _inventoryByKey = new();
        }

        public void SaveInventory(string key, Inventory inven)
        {
            _inventoryByKey[key] = inven;
        }

        public bool TryGetInventory(string key, out Inventory inven)
        {
            if (_inventoryByKey.TryGetValue(key, out inven) == false)
            {
                Debug.Log($"Key is null.\nKey : {key}");
                return false;
            }

            return true;
        }
    }
}
