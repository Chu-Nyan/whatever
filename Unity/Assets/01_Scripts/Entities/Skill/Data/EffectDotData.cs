namespace Whatever.Entities.Skill
{
    public class EffectDotData : EffectData
    {
        public const float DamageInterval = 1f;

        public int ID;
        public float DamageRate;
        public float Duration;

        public override EffectType Type
        {
            get => EffectType.Dot;
        }
    }
}
