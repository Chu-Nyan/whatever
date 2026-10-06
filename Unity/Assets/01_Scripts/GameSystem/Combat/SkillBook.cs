using System.Collections.Generic;
using Whatever.Entities.Skill;

namespace Whatever.GameSystems.Combat
{
    public class SkillBook
    {
        private readonly Dictionary<int, SkillCore> _skills;

        public SkillCore this[int id] => _skills[id];

        public SkillBook()
        {
            _skills = new();
        }

        public bool Contains(int id)
        {
            return _skills.ContainsKey(id);
        }

        public bool CanUse(int id)
        {
            if (_skills.ContainsKey(id) == false)
                return false;

            return true;
        }

        public void AddSkill(int id)
        {
            if (_skills.ContainsKey(id) == true)
                return;

            _skills.Add(id, SkillFactory.Instance.CreateKernel(id));
        }
    }
}
