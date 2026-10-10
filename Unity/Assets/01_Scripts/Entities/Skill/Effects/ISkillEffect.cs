using Whatever.GameSystems.Stat;

namespace Whatever.Entities.Skill
{
    public interface ISkillEffect
    {
        public bool IsDone { get; }

        public void Tick(Stats stats);
    }
}
