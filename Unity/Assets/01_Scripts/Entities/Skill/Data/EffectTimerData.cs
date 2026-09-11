namespace Whatever.Entities.Skill
{
    public class EffectTimerData : IEffectData
    {
        public int ID;
        public float Duration;

        public EffectType Type
        {
            get => EffectType.Timer;
        }
    }
}
