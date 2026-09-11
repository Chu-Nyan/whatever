namespace Whatever.Entities.Skill
{
    public interface ISkillEffect
    {
        public bool IsDone { get; }

        public void Tick(ISkillTarget stats);
    }
}
