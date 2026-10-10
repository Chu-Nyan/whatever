using UnityEngine;
using Whatever.GameSystems.Stat;

namespace Whatever.Entities.Skill
{
    public class EffectInstant : ISkillEffect
    {
        private EffectInstantData _data;
        private ProjectileData _context;
        private bool _isDone;

        public bool IsDone
        {
            get => _isDone;
        }

        public void Setup(EffectInstantData data, ProjectileData context)
        {
            _data = data;
            _context = context;
        }

        public void Tick(Stats stats)
        {
            stats.AddResource(ResourceType.Health, -(_context.Damage * _data.DamageRate));
            Debug.Log(stats[ResourceType.Health]);
            _isDone = true;
        }
    }
}
