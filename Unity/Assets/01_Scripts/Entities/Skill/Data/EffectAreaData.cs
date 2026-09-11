namespace Whatever.Entities.Skill
{
    public class EffectAreaData : IEffectData
    {
        public int ID;
        public float DamageRate;
        public float Ranged;
        public int Count;

        public EffectType Type
        {
            get => EffectType.Area;
        }
    }
}
