using UnityEngine;

namespace Whatever.Entities.Items
{
    public class Consumable : ItemBase
    {
        public readonly ConsumableDefinitionData ConsumableDef;
        public ConsumableInstanceData ConsumableInst;

        public Consumable(ItemDefinitionData item, ConsumableDefinitionData consumable) : base(item)
        {
            ConsumableDef = consumable;
            ConsumableInst = new();
        }

        public override bool CanUse()
        {
            return Inst.Amount > 0;
        }

        public override void Use()
        {
            Inst.Amount--;
            Debug.Log($"Inst.Amount = " + Inst.Amount);
        }
    }
}
