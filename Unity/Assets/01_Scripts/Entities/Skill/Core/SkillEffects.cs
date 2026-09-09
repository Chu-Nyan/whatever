using System.Collections.Generic;
using UnityEngine;

namespace Whatever.Entities.Skill
{
    public class SkillEffects
    {
        private readonly SkillObjectContext _context;
        private readonly List<ISkillStep> _steps;

        private int _currentStepIndex;
        private bool _isDone;

        public SkillObjectContext Context
        {
            get => _context;
        }

        public SkillEffects(SkillObjectContext context, List<ISkillStep> steps)
        {
            if (steps == null || steps.Count == 0)
                throw new System.Exception($"Invalid steps\nSkill ID : {_context.SkillData.ID}");

            _context = context;
            _steps = steps;
            _currentStepIndex = 0;
            _isDone = false;
        }

        public bool TickAndCheck(ISkillTarget stats)
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
