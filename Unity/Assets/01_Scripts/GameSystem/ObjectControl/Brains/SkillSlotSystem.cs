using System;

namespace Whatever.ObjectControl
{
    public class SkillSlotSystem
    {
        public enum Slot
        {
            Basic, Support, Dodge, Ultimate, Slot1, Slot2, Slot3, Slot4, Slot5, Slot6, Slot7, Slot8
        }

        private const int _slotCount = 12;

        private readonly int[] _skillIDBySlot;

        public int this[Slot slot] => _skillIDBySlot[(int)slot];
        public int this[int index] => _skillIDBySlot[index];

        public SkillSlotSystem()
        {
            _skillIDBySlot = new int[_slotCount];
        }

        public void CopySlot(int[] slots)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                _skillIDBySlot[i] = slots[i];
            }
        }

        public void BindingSlot(Slot type, int skillID)
        {
            _skillIDBySlot[(int)type] = skillID;
        }

        public void Clear()
        {
            Array.Clear(_skillIDBySlot, 0, _skillIDBySlot.Length);
        }
    }
}
