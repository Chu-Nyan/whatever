namespace Whatever.Entities.Skill
{
    public class EffectInstantData : IEffectData
    {
        public int ID;
        public float DamageRate;
        public int Count;

        public EffectType Type
        {
            get => EffectType.Instant;
        }
    }
}
