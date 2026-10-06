using System.Collections.Generic;
using UnityEngine;

namespace Whatever.GameSystems
{
    public class CooldownSystem
    {
        public enum Category { Skill = 100000, Item = 200000 }

        private readonly Dictionary<int, float> _cooldowns;
        private readonly List<int> _removeBuffer;

        public int Count => _cooldowns.Count;

        public CooldownSystem()
        {
            _cooldowns = new();
            _removeBuffer = new();
        }

        public bool ContainSkills(int id)
        {
            return _cooldowns.ContainsKey((int)Category.Skill + id);
        }

        public float GetRemainTime(Category category, int id)
        {
            if (_cooldowns.TryGetValue(CooldownID(category, id), out float endTime) == false)
                return 0f;

            return Mathf.Max(0f, endTime - Time.time);
        }

        public void Set(Category category, int id, float cooldown)
        {
            _cooldowns.Add(CooldownID(category, id), Time.time + cooldown);
        }

        public void Tick()
        {
            if (_cooldowns.Count <= 0)
                return;

            var time = Time.time;

            foreach (var item in _cooldowns)
            {
                if (item.Value <= time)
                {
                    _removeBuffer.Add(item.Key);
                }
            }

            if (_removeBuffer.Count > 0)
            {
                foreach (var item in _removeBuffer)
                {
                    _cooldowns.Remove(item);
                }
                _removeBuffer.Clear();
            }
        }

        public static int CooldownID(Category category, int id)
        {
            return (int)category + id;
        }

    }
}
