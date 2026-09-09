namespace Whatever.Entities.Skill
{
    public class StepInstant : ISkillStep
    {
        private EffectInstantData _data;
        private SkillObjectContext _context;
        private bool _isDone;

        public bool IsDone
        {
            get => _isDone;
        }

        public void Setup(EffectInstantData data, SkillObjectContext context)
        {
            _data = data;
            _context = context;
        }

        public void Tick(ISkillTarget stats)
        {
            // TODO : 처리
            _isDone = true;
        }
    }
}
