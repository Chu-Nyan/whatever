using System.Collections.Generic;
using UnityEngine;
using Whatever.Entities.Items;
using Whatever.ObjectControl;

namespace Whatever.GameSystems
{
    public class PersistentDataContainer
    {
        private readonly Dictionary<string, CategoryInventory> _categoryInvenByKey;
        private readonly Dictionary<string, QuickSlotInventory> _quickSlotInvenByKey;
        private readonly Dictionary<string, QuickSlotLoadout> _quickSlotByKey;

        public PersistentDataContainer()
        {
            _categoryInvenByKey = new();
            _quickSlotInvenByKey = new();
            _quickSlotByKey = new();
        }

        public void SaveCategoryInventory(string key, CategoryInventory inven)
        {
            _categoryInvenByKey[key] = inven;
        }

        public void SaveQuickSlotInventory(string key, QuickSlotInventory inven)
        {
            _quickSlotInvenByKey[key] = inven;
        }

        public void SaveQuickSlotLoadout(string key, QuickSlotLoadout loadout)
        {
            _quickSlotByKey[key] = loadout;
        }

        public bool TryGetQuickSlotInventory(string key, out QuickSlotInventory inven)
        {
            if (_quickSlotInvenByKey.TryGetValue(key, out inven) == false)
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
