using Chunity.Core;
using System;

namespace Whatever.Entities.Skill
{
    public class SkillCore
    {
        public readonly int ID;
        private SkillBaseData _data;
        private float _remainingCooldown;
        private bool _canUse;

        public float RemainingCooldown => _remainingCooldown;

        public bool CanUse
        {
            get => _canUse;
        }

        public SkillBaseData Data
        {
            get => _data;
        }

        public SkillCore()
        {
            ID = IDGenerator.Next();
        }

        public void Setup(SkillBaseData data)
        {
            _data = data;
            _remainingCooldown = 0;
            _canUse = true;
        }

        public bool TryUse()
        {
            if (CanUse == false)
                return false;

            Use();
            return true;
        }

        public void Use()
        {
            _remainingCooldown = _data.Cooldown;
            _canUse = false;
        }

        public void ReduceCooldown(float time)
        {
            _remainingCooldown = Math.Max(0f, _remainingCooldown - time);
            if (_remainingCooldown <= 0)
                _canUse = true;
        }
    }
}
