using Chunity.Core;
using Chunity.GameSystem.Physics;
using Chunity.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;
using Whatever.Core;
using Whatever.GameSystems;

namespace Whatever.Entities.Skill
{
    public class SkillFactory : Singleton<SkillFactory>
    {
        private readonly Transform _root;
        private readonly ObjectPooling<Projectile> _objectPool;

        private readonly List<ISkillEffect> _effectBuffer = new();
        private readonly Dictionary<EffectType, Func<ProjectileData, EffectData, ISkillEffect>> _generateFuncByProcessType;


        public SkillFactory()
        {
            _root = new GameObject("Skill").transform;
            _objectPool = new(() => AssetManager.InstantiateAssetSync<Projectile>("SkillObject", "SkillObject", _root));

            _generateFuncByProcessType = new()
            {
                { EffectType.Instant, CreateStepInstant },
                { EffectType.Dot, CreateStepDot },
                { EffectType.Area, CreateStepArea },
                { EffectType.Timer, CreateStepTimer },
            };
        }


        public SkillCore CreateKernel(int id)
        {
            var kernel = new SkillCore();
            kernel.Setup(DataBase.Instance.SkillRepo.Skills[id]);

            return kernel;
        }

        public Projectile CreateObject(int instigatorID, int skillID, float dmg, FactionType faction, Vector3 start, Vector3 dir)
        {
            var obj = _objectPool.Dequeue();
            obj.Destroyed += _objectPool.Enqueue;

            var skillData = DataBase.Instance.SkillRepo.Skills[skillID];
            var targetFaction = DataBase.Instance.WorldProfile.FactionTable[faction].HostilityMask;
            var mask = new NyanLayerMask();
            obj.Setup(instigatorID, skillData, dmg, targetFaction, mask);
            obj.SetTarget(start, dir);
            obj.SetActive(true);
            return obj;
        }

        public EffectProcess CreateEffects(ProjectileData data, int hitSequence)
        {
            var effectData = data.SkillData.SequenceData[hitSequence].EffectDatas;
            for (int i = 0; i < effectData.Length; i++)
            {
                var type = effectData[i].Type;
                _effectBuffer.Add(_generateFuncByProcessType[type].Invoke(data, effectData[i]));
            }

            var effects = new EffectProcess();
            effects.Setup(data, _effectBuffer);

            _effectBuffer.Clear();
            return effects;
        }

        private ISkillEffect CreateStepInstant(ProjectileData data, EffectData effectData)
        {
            Debug.Assert(effectData is EffectInstantData, $"Skill ID : {data.SkillData.ID}");
            var step = new EffectInstant();
            step.Setup((EffectInstantData)effectData, data);
            return step;
        }

        private ISkillEffect CreateStepDot(ProjectileData data, EffectData effectData)
        {
            Debug.Assert(effectData is EffectDotData, $"Skill ID : {data.SkillData.ID}");
            var step = new EffectDot();
            step.Setup((EffectDotData)effectData, data);
            return step;
        }

        private ISkillEffect CreateStepArea(ProjectileData data, EffectData effectData)
        {
            Debug.Assert(effectData is EffectAreaData, $"Skill ID : {data.SkillData.ID}");
            var step = new EffectArea();
            step.Setup((EffectAreaData)effectData, data);
            return step;
        }

        private ISkillEffect CreateStepTimer(ProjectileData data, EffectData effectData)
        {
            Debug.Assert(effectData is EffectTimerData, $"Skill ID : {data.SkillData.ID}");
            var step = new EffectTimer();
            step.Setup((EffectTimerData)effectData, data);
            return step;
        }
    }
}
