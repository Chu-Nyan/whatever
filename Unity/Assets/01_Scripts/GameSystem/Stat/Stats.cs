using System;

namespace Whatever.GameSystems.Stat
{
    public class Stats
    {
        public const int StatCount = (int)StatType.WaterRes + 1;

        private float[] _bases;
        private readonly float[] _add;
        private readonly float[] _multiply;
        private readonly float[] _current;

        private readonly Resource[] _resources;

        public float this[StatType type]
        {
            get => _bases[(int)type];
        }

        public float this[ResourceType type]
        {
            get => _resources[(int)type].Value;
        }

        public Stats()
        {
            _add = new float[StatCount];
            _multiply = new float[StatCount];
            _current = new float[StatCount];
            _resources = new Resource[((int)ResourceType.Stamina) + 1];
            for (int i = 0; i < _resources.Length; i++)
            {
                _resources[i] = new();
            }
        }

        public void SetupBase(float[] baseStats)
        {
            if (baseStats.Length != StatCount)
                throw new Exception();

            _bases = baseStats;
            ResetModifier();
            ResetResource();
        }

        public void ApplyModifier(StatModifier modifier)
        {
            int statIndex = (int)modifier.Stat;
            if (modifier.Modifier == ModifierType.Add)
            {
                _add[statIndex] += modifier.Value;
            }
            else // modifier.Modifier == ModifierType.Multiply
            {
                _multiply[statIndex] *= modifier.Value;
            }

            _current[statIndex] = Calculate(statIndex, _bases[statIndex]);
        }

        public void AddResource(ResourceType type, float value)
        {
            switch (type)
            {
                case ResourceType.Health:
                    _resources[(int)type].Add(value, _current[(int)StatType.Health]);
                    break;
                case ResourceType.Mana:
                    _resources[(int)type].Add(value, _current[(int)StatType.Mana]);
                    break;
                case ResourceType.Stamina:
                    _resources[(int)type].Add(value, _current[(int)StatType.Stamina]);
                    break;
            }
        }

        public void ResetModifier()
        {
            for (int i = 0; i < _bases.Length; i++)
            {
                _add[i] = 0;
                _multiply[i] = 0;
                _current[i] = _bases[i];
            }
        }

        public void ResetResource()
        {
            _resources[(int)ResourceType.Health].Set(_current[(int)StatType.Health]);
            _resources[(int)ResourceType.Mana].Set(_current[(int)StatType.Mana]);
            _resources[(int)ResourceType.Stamina].Set(_current[(int)StatType.Stamina]);
        }

        private float Calculate(int index, float def)
        {
            return (def + _add[index]) * _multiply[index];
        }
    }
}
