namespace Whatever.Entities.Items
{
    public class Equipment : ItemBase
    {
        public readonly EquipmentDefinitionData EquipmentDef;
        public EquipmentInstanceData EquipmentInst;

        public Equipment(ItemDefinitionData item, EquipmentDefinitionData equipment) : base(item)
        {
            EquipmentDef = equipment;
            EquipmentInst = new();
        }

        public override bool CanUse()
        {
            return true;
        }

        public override void Use()
        {
            EquipmentInst.IsEquipped = !EquipmentInst.IsEquipped;
        }
    }
}
