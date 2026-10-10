using Chunity.GameSystem.Physics;
using System;
using System.Collections.Generic;
using UnityEngine;
using Whatever.Entities.Skill;
using Whatever.GameSystems.Stat;

namespace Whatever.GameSystems.Combat
{
    public class CombatSystem : INyanCollisionProvider
    {
        public readonly int InstigatorID;
        private readonly NyanCollider _collider;
        private readonly List<EffectProcess> _skillEffects;

        public event Action<SkillBaseData, float> SkillHit;

        public NyanCollider Collider => _collider;

        public CombatSystem(int instigatorID, NyanLayerMask mask)
        {
            InstigatorID = instigatorID;
            _collider = NyanColliderFactory.Instance.Create(this, $"Character {instigatorID}");
            _collider.SetInstigatorID(InstigatorID);
            _collider.SetLayer(NyanLayer.Character, mask);
            _collider.SetActive(true);
            _skillEffects = new();
        }

        public void Tick(Stats stats)
        {
            for (int i = _skillEffects.Count - 1; i >= 0; i--)
            {
                if (_skillEffects[i].TickAndCheck(stats) == false)
                    continue;

                _skillEffects.RemoveAt(i);
            }
        }

        public void SetBodyShape(IShape shape)
        {
            _collider.SetShape(shape);
        }

        public bool FireSkill(int skillID, Vector3 start, Vector3 dir)
        {
            SkillFactory.Instance.CreateObject(InstigatorID, skillID, 5, FactionType.Player, start, dir);
            return true;
        }

        public void OnNyanCollisionEnter(INyanCollisionProvider provider)
        {
            if (provider.Collider.Layer == NyanLayer.Projectile)
            {
                if (provider is Projectile projectile == false)
                    throw new Exception();

                _skillEffects.Add(SkillFactory.Instance.CreateEffects(projectile.ProjectileData, projectile.LogicStep));
            }
        }

        public void OnNyanCollisionExit(INyanCollisionProvider provider)
        {
        }
    }
}
