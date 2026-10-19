using UnityEngine;
using Whatever.Entities.Items;
using Whatever.GameSystems;
using Whatever.GameSystems.Combat;

namespace Whatever.ObjectControl
{
    public interface IOffenseReceiver : IInputReceiver
    {
        public Transform transform { get; }
        public CombatSystem CombatSystem { get; }
        public QuickSlotInventory Inventory { get; }
        public SkillBook SkillBook { get; }
        public CooldownSystem CooldownSys { get; }
        public void UseSkill(int skillID, Vector3 targetPoint);
        public void UseItem(int itemID);
    }
}
