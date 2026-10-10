using System.Collections.Generic;
using UnityEngine;
using Whatever.GameSystems.Stat;

namespace Whatever.Entities.Skill
{
    public class EffectProcess
    {
        private ProjectileData _data;
        private readonly List<ISkillEffect> _steps;

        private int _currentStepIndex;
        private bool _isDone;

        public EffectProcess()
        {
            _steps = new();
        }

        public void Setup(ProjectileData data, List<ISkillEffect> steps)
        {
            if (steps == null || steps.Count == 0)
                throw new System.Exception($"Invalid steps\nSkill ID : {data.SkillData.ID}");
            _data = data;
            _steps.Clear();
            for (int i = 0; i < steps.Count; i++)
            {
                _steps.Add(steps[i]);
            }
            _currentStepIndex = 0;
            _isDone = false;
        }

        public bool TickAndCheck(Stats stats)
        {
            if (_isDone == true)
            {
                Debug.LogWarning("완료된 Sequence 진입");
                return true;
            }

            while (_currentStepIndex < _steps.Count)
            {
                var step = _steps[_currentStepIndex];

                if (step.IsDone == false)
                    step.Tick(stats);

                if (step.IsDone == true)
                    _currentStepIndex++;
                else
                    return false;
            }

            _isDone = true;
            return true;
        }
    }
}
