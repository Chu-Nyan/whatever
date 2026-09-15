namespace Whatever.Entities.Items
{
    public class Equipment : ItemBase
    {
        public readonly EquipmentDefinitionData _equipmentDef;
        public EquipmentInstanceData _equipmentInst;

        public Equipment(ItemDefinitionData item, EquipmentDefinitionData equipment) : base(item)
        {
            _equipmentDef = equipment;
            _equipmentInst = new();
        }

        public override bool CanUse()
        {
            return true;
        }

        public override void Use()
        {
            _equipmentInst.IsEquipped = !_equipmentInst.IsEquipped;
        }
    }
}
