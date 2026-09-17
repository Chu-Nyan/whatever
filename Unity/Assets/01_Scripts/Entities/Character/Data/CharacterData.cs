namespace Whatever.Entities.Characters
{
    public class CharacterData
    {
        public CharacterDefinitionData Def;
        public StatsData Ins;

        public CharacterData()
        {
            Ins = new();
        }

        public void Setup(CharacterDefinitionData def)
        {
            Def = def;
            Ins.CopyFrom(def.Stats);
        }
    }
}
