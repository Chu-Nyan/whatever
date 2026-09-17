using Whatever.Entities.Characters;

namespace Whatever.Core
{
    public class CharacterRepository
    {
        public readonly CharacterDefinitionData[] DefinitionData;

        public CharacterRepository()
        {
            DefinitionData = DataBase.ConvertToData<CharacterDefinitionDto, CharacterDefinitionData>("CharacterDefinitionData", a => a.ToData());
            var stats = DataBase.ConvertToData<CharacterStatsDto, float[]>("CharacterStatsData", a => a.ToData());
            for (int i = 1; i < DefinitionData.Length; i++)
            {
                DefinitionData[i].Stats = stats[i];
            }
        }
    }
}
