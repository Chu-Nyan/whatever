namespace Whatever.Entities.Skill
{
    public interface ISkillTarget
    {
        public void ApplySkillEffect(SkillBaseData data, EffectProcess effect);
    }
}
