namespace Whatever.Entities.Skill
{
    public interface ISkillStep
    {
        public bool IsDone { get; }

        public void Tick(ISkillTarget stats);
    }
}
