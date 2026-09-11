namespace Whatever.Entities.Skill
{
    public class EffectAreaData : EffectData
    {
        public int ID;
        public float DamageRate;
        public float Ranged;
        public int Count;

        public override EffectType Type
        {
            get => EffectType.Area;
        }
    }
}
