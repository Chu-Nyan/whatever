namespace Whatever.Entities.Characters
{
    public class StatsData
    {
        public float Health;
        public float HealthRegen;
        public float Mana;
        public float ManaRegen;
        public float Stamina;
        public float StaminaRegen;
        public float AttackPower;
        public float MagicPower;
        public float Defense;
        public float MagicDefense;
        public float MoveSpeed;
        public float FireRes;
        public float IceRes;
        public float LightningRes;
        public float WaterRes;

        public void CopyFrom(StatsData source)
        {
            Health = source.Health;
            HealthRegen = source.HealthRegen;
            Mana = source.Mana;
            ManaRegen = source.ManaRegen;
            Stamina = source.Stamina;
            StaminaRegen = source.StaminaRegen;
            AttackPower = source.AttackPower;
            MagicPower = source.MagicPower;
            Defense = source.Defense;
            MagicDefense = source.MagicDefense;
            MoveSpeed = source.MoveSpeed;
            FireRes = source.FireRes;
            IceRes = source.IceRes;
            LightningRes = source.LightningRes;
            WaterRes = source.WaterRes;
        }
    }
}
