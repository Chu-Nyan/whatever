using Whatever.GameSystems.Stat;

namespace Whatever.Entities.Characters
{
    public class CharacterData
    {
        public CharacterDefinitionData Def;
        public Stats StatIns;

        public CharacterData()
        {
            StatIns = new();
        }

        public void Setup(CharacterDefinitionData def)
        {
            Def = def;
            StatIns.SetupBase(def.Stats);
        }
    }
}
