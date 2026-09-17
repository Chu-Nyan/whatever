using Whatever.Entities.Characters;
using Whatever.Entities.Items;

namespace Whatever.Core
{
    public class ItemRepository
    {
        public readonly ItemDefinitionData[] ItemDefinition;
        public readonly ConsumableDefinitionData[] ConsumableDefinition;
        public readonly EquipmentDefinitionData[] EquipmentDefinition;

        public ItemRepository()
        {
            ItemDefinition = DataBase.ConvertToData<ItemBaseDto, ItemDefinitionData>("ItemBaseData", a => a.ToData());
            ConsumableDefinition = DataBase.ConvertToData<ItemConsumableDto, ConsumableDefinitionData>("ItemConsumableData", a => a.ToData());
            EquipmentDefinition = DataBase.ConvertToData<ItemEquipmentDto, EquipmentDefinitionData>("ItemEquipmentData", a => a.ToData());

            var equipmentStats = DataBase.ConvertToDictionary<ItemEquipmentStatsDto, StatModifier>("ItemEquipmentStatsData", a => a.ToData());
            for (int i = 1; i < EquipmentDefinition.Length; i++)
            {
                var equipment = EquipmentDefinition[i];
                equipment.StatModifiers = equipmentStats[equipment.ID];
            }
        }
    }
}
