namespace Whatever.ObjectControl
{
    public class QuickSlotLoadout
    {
        public enum SlotType
        {
            Basic, Support, Slot1, Slot2, Slot3, Slot4, Slot5, Slot6, Slot7, Slot8, Dodge, Ultimate, Q, E
        }

        public const int SkillCount = 12;
        public const int ItemCount = 2;

        private readonly int[] _idByType;

        public int this[SlotType slot]
        {
            get => _idByType[(int)slot];
        }

        public int this[int slot]
        {
            get => _idByType[slot];
        }

        public QuickSlotLoadout()
        {
            _idByType = new int[SkillCount + ItemCount];
        }

        public void CopySlot(int[] skillSlot)
        {
            for (int i = 0; i < skillSlot.Length; i++)
            {
                BindingSlot((SlotType)i, skillSlot[i]);
            }
        }

        public void BindingSlot(SlotType type, int skillID)
        {
            _idByType[(int)type] = skillID;
        }
    }
}
