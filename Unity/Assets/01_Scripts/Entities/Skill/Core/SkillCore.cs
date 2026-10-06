using Chunity.Core;

namespace Whatever.Entities.Skill
{
    public class SkillCore
    {
        public readonly int InstanceID;
        private SkillBaseData _data;

        public SkillBaseData Data
        {
            get => _data;
        }

        public SkillCore()
        {
            InstanceID = IDGenerator.Next();
        }

        public void Setup(SkillBaseData data)
        {
            _data = data;
        }
    }
}
