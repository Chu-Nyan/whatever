namespace Whatever.Entities.Skill
{
    public class EffectDotData : IEffectData
    {
        public const float DamageInterval = 1f;

        public int ID;
        public float DamageRate;
        public float Duration;

        public EffectType Type
        {
            get => EffectType.Dot;
        }
    }
}
