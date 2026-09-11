using System.Collections.Generic;
using UnityEngine;
using Whatever.Entities.Skill;

namespace Whatever.GameSystems.Combat
{
    public class SkillBook
    {
        private readonly Dictionary<int, SkillCore> _skills;
        private readonly List<SkillCore> _used;

        public SkillBook()
        {
            _skills = new();
            _used = new();
        }

        public bool CanUse(int id)
        {
            if (_skills.ContainsKey(id) == false)
                return false;
            if (_skills[id].CanUse == false)
                return false;

            return true;
        }

        public void AddSkill(int id)
        {
            if (_skills.ContainsKey(id) == true)
                return;

            _skills.Add(id, SkillFactory.Instance.CreateKernel(id));
        }

        public void ForceUseSkill(int id)
        {
            _skills[id].Use();
            _used.Add(_skills[id]);
        }

        public void Tick()
        {
            if (_used.Count <= 0)
                return;

            var time = Time.deltaTime;
            for (int i = _used.Count - 1; i >= 0; i--)
            {
                var skill = _used[i];
                skill.ReduceCooldown(time);
                if (skill.CanUse == true)
                    _used.RemoveAt(i);
            }
        }

    }
}
