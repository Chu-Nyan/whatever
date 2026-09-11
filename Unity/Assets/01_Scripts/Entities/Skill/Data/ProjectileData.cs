using Chunity.GameSystem.Physics;

namespace Whatever.Entities.Skill
{
    public class ProjectileData
    {
        public SkillBaseData SkillData;
        public int TargetFactionMask;
        public NyanLayerMask Mask;

        public float Damage;

        public void Setup(SkillBaseData data, float damage, int targetFactionMask, NyanLayerMask mask)
        {
            SkillData = data;
            Damage = damage;

            TargetFactionMask = targetFactionMask;
            Mask = mask;
        }
    }
}
