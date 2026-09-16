using Chunity.GameSystem.Physics;
using Whatever.Entities.Skill;

namespace Whatever.Core
{
    public class SkillRepository
    {
        public readonly SkillBaseData[] Skills;
        public readonly int[][] SkillSet;

        private readonly SequenceData[][] _sequenceDatas;
        private readonly IEffectData[][] _effectData;
        private readonly ShapeParam[][] _hitboxesData;
        private readonly EffectInstantData[] _instants;
        private readonly EffectDotData[] _dots;
        private readonly EffectAreaData[] _areas;
        private readonly EffectTimerData[] _timers;

        public SkillRepository()
        {
            _timers = DataBase.ConvertToData<SkillEffectTimerDto, EffectTimerData>("SkillEffectTimerData", a => a.ToData());
            _areas = DataBase.ConvertToData<SkillEffectAreaDto, EffectAreaData>("SkillEffectAreaData", a => a.ToData());
            _dots = DataBase.ConvertToData<SkillEffectDotDto, EffectDotData>("SkillEffectDotData", a => a.ToData());
            _instants = DataBase.ConvertToData<SkillEffectInstantDto, EffectInstantData>("SkillEffectInstantData", a => a.ToData());
            _hitboxesData = DataBase.ConvertToArray<SkillHitboxDto, ShapeParam>("SkillHitboxData", a => a.ToData());

            _effectData = DataBase.ConvertToArray<SkillEffectDto, IEffectData>("SkillEffectData", a => GetEffectData(a.EffectType, a.EffectID));
            _sequenceDatas = DataBase.ConvertToArray<SkillSequenceDto, SequenceData>("SkillSequenceData", a => a.ToData(_hitboxesData[a.HitboxID], _effectData[a.EffectID]));
            Skills = DataBase.ConvertToData<SkillBaseDto, SkillBaseData>("SkillBaseData", a => a.ToData(_sequenceDatas[a.CollisionSequenceID]));

            SkillSet = DataBase.ConvertToArray<SkillSetDto, int>("SkillSetData", a => a.SkillID);
        }

        private IEffectData GetEffectData(EffectType type, int id)
        {
            switch (type)
            {
                case EffectType.Instant:
                    return _instants[id];
                case EffectType.Dot:
                    return _dots[id];
                case EffectType.Area:
                    return _areas[id];
                case EffectType.Timer:
                    return _timers[id];
                default:
                    throw new System.Exception();
            }
        }
    }
}
