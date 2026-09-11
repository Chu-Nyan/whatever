using UnityEngine;

namespace Whatever.Entities.Skill
{
    public class StepTimer : ISkillStep
    {
        private EffectTimerData _data;
        private float _remainTime;
        private bool _isDone;

        public bool IsDone
        {
            get => _isDone;
        }

        public void Setup(EffectTimerData data, ProjectileData context)
        {
            _data = data;
            _remainTime = _data.Duration;
            _isDone = false;
        }

        public void Tick(ISkillTarget stats)
        {
            _remainTime -= Time.deltaTime;

            if (_remainTime <= 0f)
                _isDone = true;
        }
    }
}
