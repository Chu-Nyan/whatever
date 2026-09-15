namespace Whatever.ObjectControl
{
    public class QuickSlotLoadout
    {
        public enum SkillSlot
        {
            Basic, Support, Dodge, Ultimate, Slot1, Slot2, Slot3, Slot4, Slot5, Slot6, Slot7, Slot8
        }

        public enum ItemSlot
        {
            Q, E
        }

        private readonly int[] _skillIDBySlot;
        private readonly int[] _itemIDBySlot;

        public int this[SkillSlot slot]
        {
            get => _skillIDBySlot[(int)slot];
        }

        public int this[ItemSlot slot]
        {
            get => _itemIDBySlot[(int)slot];
        }

        public QuickSlotLoadout()
        {
            _skillIDBySlot = new int[12];
            _itemIDBySlot = new int[2];
        }

        public void CopySkillSlot(int[] skillSlot)
        {
            for (int i = 0; i < skillSlot.Length; i++)
            {
                _skillIDBySlot[i] = skillSlot[i];
            }
        }

        public void CopyItemSlot(int[] itemSlot)
        {
            for (int i = 0; i < itemSlot.Length; i++)
            {
                _itemIDBySlot[i] = itemSlot[i];
            }
        }

        public void BindingSlot(SkillSlot type, int skillID)
        {
            _skillIDBySlot[(int)type] = skillID;
        }

        public void BindingSlot(ItemSlot type, int skillID)
        {
            _itemIDBySlot[(int)type] = skillID;
        }
    }
}
