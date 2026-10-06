using System.Collections.Generic;
using UnityEngine;
using Whatever.Entities.Items;
using Whatever.ObjectControl;

namespace Whatever.GameSystems
{
    public class PersistentDataContainer
    {
        private readonly Dictionary<string, Inventory> _inventoryByKey;
        private readonly Dictionary<string, QuickSlotLoadout> _quickSlotByKey;

        public PersistentDataContainer()
        {
            _inventoryByKey = new();
            _quickSlotByKey = new();
        }

        public void SaveInventory(string key, Inventory inven)
        {
            _inventoryByKey[key] = inven;
        }

        public void SaveQuickSlot(string key, QuickSlotLoadout loadout)
        {
            _quickSlotByKey[key] = loadout;
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

        public QuickSlotLoadout GetQuickSlot(string key)
        {
            return _quickSlotByKey[key];
        }
    }
}
