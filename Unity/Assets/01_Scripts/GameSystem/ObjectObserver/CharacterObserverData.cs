using Whatever.GameSystems.Stat;

namespace Whatever.GameSystems
{
    public class CharacterObserverData
    {
        public readonly float[] BeforeBases;
        public readonly float[] BeforeResources;

        public bool HasUsedSkill;

        public CharacterObserverData()
        {
            BeforeBases = new float[Stats.StatCount];
            BeforeResources = new float[3];
        }

        public void CaptureStats(Stats stats)
        {
            BeforeBases[(int)StatType.Health] = stats[StatType.Health];
            BeforeBases[(int)StatType.Mana] = stats[StatType.Mana];
            BeforeBases[(int)StatType.Stamina] = stats[StatType.Stamina];

            BeforeResources[(int)ResourceType.Health] = stats[ResourceType.Health];
            BeforeResources[(int)ResourceType.Mana] = stats[ResourceType.Mana];
            BeforeResources[(int)ResourceType.Stamina] = stats[ResourceType.Stamina];
        }
    }
}
