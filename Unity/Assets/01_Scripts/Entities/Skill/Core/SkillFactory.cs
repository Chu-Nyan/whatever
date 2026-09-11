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
        private readonly ObjectPooling<SkillObject> _objectPool;

        private readonly List<ISkillStep> _effectBuffer = new();
        private readonly Dictionary<EffectType, Func<ProjectileData, EffectData, ISkillStep>> _generateFuncByProcessType;


        public SkillFactory()
        {
            _root = new GameObject("Skill").transform;
            _objectPool = new(() => AssetManager.InstantiateAssetSync<SkillObject>("SkillObject", "SkillObject", _root));

            _generateFuncByProcessType = new()
            {
                { EffectType.Instant, CreateStepInstant },
                { EffectType.Dot, CreateStepDot },
                { EffectType.Area, CreateStepArea },
                { EffectType.Timer, CreateStepTimer },
            };
        }


        public SkillKernel CreateKernel(int id)
        {
            var kernel = new SkillKernel();
            kernel.Setup(DataBase.Instance.SkillRepo.Skills[id]);

            return kernel;
        }

        public SkillObject CreateObject(int instigatorID, int skillID, float dmg, FactionType faction, Vector3 start, Vector3 dir)
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

        public SkillEffects CreateEffects(ProjectileData data, int hitSequence)
        {
            var effectData = data.SkillData.SequenceData[hitSequence].EffectDatas;
            for (int i = 0; i < effectData.Length; i++)
            {
                var type = effectData[i].Type;
                _effectBuffer.Add(_generateFuncByProcessType[type].Invoke(data, effectData[i]));
            }

            var effects = new SkillEffects();
            effects.Setup(data, _effectBuffer);

            _effectBuffer.Clear();
            return effects;
        }

        private ISkillStep CreateStepInstant(ProjectileData data, EffectData effectData)
        {
            Debug.Assert(effectData is EffectInstantData, $"Skill ID : {data.SkillData.ID}");
            var step = new StepInstant();
            step.Setup((EffectInstantData)effectData, data);
            return step;
        }

        private ISkillStep CreateStepDot(ProjectileData data, EffectData effectData)
        {
            Debug.Assert(effectData is EffectDotData, $"Skill ID : {data.SkillData.ID}");
            var step = new SkillStepDot();
            step.Setup((EffectDotData)effectData, data);
            return step;
        }

        private ISkillStep CreateStepArea(ProjectileData data, EffectData effectData)
        {
            Debug.Assert(effectData is EffectAreaData, $"Skill ID : {data.SkillData.ID}");
            var step = new StepArea();
            step.Setup((EffectAreaData)effectData, data);
            return step;
        }

        private ISkillStep CreateStepTimer(ProjectileData data, EffectData effectData)
        {
            Debug.Assert(effectData is EffectTimerData, $"Skill ID : {data.SkillData.ID}");
            var step = new StepTimer();
            step.Setup((EffectTimerData)effectData, data);
            return step;
        }
    }
}
