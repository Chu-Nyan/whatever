namespace Whatever.Entities.Skill
{
    public class EffectTimerData : EffectData
    {
        public int ID;
        public float Duration;

        public override EffectType Type
        {
            get => EffectType.Timer;
        }
    }
}
