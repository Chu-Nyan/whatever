using UnityEngine;

namespace Whatever.Entities.Skill
{
    public class EffectDot : ISkillEffect
    {
        private EffectDotData _data;
        private ProjectileData _context;
        private bool _isDone;
        private float _remainInterval;
        private float _remainTick;

        public bool IsDone
        {
            get => _isDone;
        }

        public void Setup(EffectDotData data, ProjectileData context)
        {
            _data = data;
            _context = context;
            _isDone = false;
            _remainInterval = EffectDotData.DamageInterval;
            _remainTick = Mathf.CeilToInt(_data.Duration / EffectDotData.DamageInterval);
        }

        public void Tick(ISkillTarget defensive)
        {
            _remainInterval -= Time.deltaTime;
            if (_remainInterval <= 0)
            {
                _remainTick--;
                _remainInterval += EffectDotData.DamageInterval;
                // TODO 

                if (_remainTick <= 0)
                    _isDone = true;
            }
        }
    }
}
