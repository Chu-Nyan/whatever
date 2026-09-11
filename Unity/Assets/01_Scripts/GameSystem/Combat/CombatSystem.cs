using UnityEngine;
using Whatever.Entities.Skill;

namespace Whatever.GameSystems.Combat
{
    public class CombatSystem
    {
        public readonly int InstigatorID;

        public CombatSystem(int instigatorID)
        {
            InstigatorID = instigatorID;
        }

        public bool FireSkill(int skillID, Vector3 start, Vector3 dir)
        {
            SkillFactory.Instance.CreateObject(InstigatorID, skillID, 5, FactionType.Player, start, dir);
            return true;
        }
    }
}
