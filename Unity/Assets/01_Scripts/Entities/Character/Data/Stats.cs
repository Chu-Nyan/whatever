using System;

namespace Whatever.Entities.Characters
{
    public class Stats
    {
        public const int StatCount = (int)StatType.WaterRes + 1;
        private static readonly float[] _defaultStats = new float[StatCount];

        private float[] _bases;

        public float this[StatType type]
        {
            get => _bases[(int)type];
        }

        public Stats()
        {
            _bases = _defaultStats;
        }

        public Stats(float[] baseStats)
        {
            _bases = baseStats;
        }

        public void SetupBase(float[] baseStats)
        {
            if (baseStats.Length != StatCount)
                throw new Exception();

            _bases = baseStats;
        }
    }

}
