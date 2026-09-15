namespace Whatever.Entities.Items
{
    public abstract class ItemBase
    {
        public readonly ItemDefinitionData Def;
        public ItemInstancedata Inst;

        public ItemBase(ItemDefinitionData item)
        {
            Def = item;
            Inst = new();
        }

        public abstract bool CanUse();
        public abstract void Use();

        public virtual bool CanRemove()
        {
            return Inst.Amount <= 0;
        }
    }
}
