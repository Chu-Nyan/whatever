using Whatever.GameSystems.Stat;

namespace Whatever.Entities.Skill
{
    public class EffectArea : ISkillEffect
    {
        private EffectAreaData _data;
        private ProjectileData _context;
        private bool _isDone;

        public bool IsDone
        {
            get => _isDone;
        }

        public void Setup(EffectAreaData data, ProjectileData context)
        {
            _data = data;
            _context = context;

        }

        public void Tick(Stats stats)
        {
            // TODO : 범위 스킬 로직
            _isDone = true;
        }
    }
}
