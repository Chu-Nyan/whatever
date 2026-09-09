namespace Whatever.Entities.Skill
{
    public class StepArea : ISkillStep
    {
        private EffectAreaData _data;
        private SkillObjectContext _context;
        private bool _isDone;

        public bool IsDone
        {
            get => _isDone;
        }

        public void Setup(EffectAreaData data, SkillObjectContext context)
        {
            _data = data;
            _context = context;

        }

        public void Tick(ISkillTarget defensive)
        {
            // TODO : 범위 스킬 로직
            _isDone = true;
        }
    }
}
