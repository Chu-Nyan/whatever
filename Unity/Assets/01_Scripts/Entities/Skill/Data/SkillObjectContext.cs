using Chunity.GameSystem.Physics;
using Whatever.GameSystems;

namespace Whatever.Entities.Skill
{
    public struct SkillObjectContext
    {
        public SkillBaseData SkillData;
        public FactionType Faction;
        public NyanLayerMask Mask;

        public float Damage;

        public SkillObjectContext(SkillBaseData data, FactionType faction, NyanLayerMask mask, float damage)
        {
            SkillData = data;
            Faction = faction;

            Mask = mask;
            Damage = damage;
        }
    }
}
