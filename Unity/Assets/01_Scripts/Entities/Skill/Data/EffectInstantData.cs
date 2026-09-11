namespace Whatever.Entities.Skill
{
    public class EffectInstantData : EffectData
    {
        public int ID;
        public float DamageRate;
        public int Count;

        public override EffectType Type
        {
            get => EffectType.Instant;
        }
    }
}
