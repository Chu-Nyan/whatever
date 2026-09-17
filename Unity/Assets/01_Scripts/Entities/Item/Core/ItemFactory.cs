using Chunity.Utility;
using System;
using UnityEngine;
using Whatever.Core;

namespace Whatever.Entities.Items
{
    public class ItemFactory : Singleton<ItemFactory>
    {
        public ItemBase Create(int itemID, int stack)
        {
            var repo = DataBase.Instance.ItemRepo;
            var data = repo.ItemDefinition[itemID];
            switch (data.Category)
            {
                case ItemCategory.Consumable:
                    var consumableData = repo.ConsumableDefinition[itemID];
                    var consumable = new Consumable(data, consumableData);
                    consumable.Inst.Amount = stack;
                    return consumable;
                case ItemCategory.Equipment:
                    var equipmentData = repo.EquipmentDefinition[itemID];
                    var equipment = new Equipment(data, equipmentData);
                    equipment.Inst.Amount = 1;
                    Debug.Assert(stack == 1, $"Invalid request.\nID : {itemID}, Requested Stack : {stack}");
                    return equipment;
                default:
                    throw new Exception();
            }
        }
    }
}
