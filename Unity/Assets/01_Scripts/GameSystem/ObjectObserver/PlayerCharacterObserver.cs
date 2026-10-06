using System;
using UnityEngine;
using Whatever.Entities.Characters;
using Whatever.GameSystems.Stat;

namespace Whatever.GameSystems
{
    public class PlayerCharacterObserver : MonoBehaviour
    {
        private CharacterObserverData _observerData;
        private Character _character;

        public event Action<Character> CharacterChanged;
        public event Action<Stats> StatsChanged;
        public event Action<CooldownSystem> SkillCooldownChanged;

        private void Awake()
        {
            _observerData = new();
        }

        public void SetCharacter(Character character)
        {
            _character = character;
            _observerData.CaptureStats(_character.Data.StatIns);

            CharacterChanged?.Invoke(character);
            StatsChanged?.Invoke(character.Data.StatIns);
        }

        private void LateUpdate()
        {
            if (IsStatsChanged(_character.Data.StatIns) == false)
            {
                _observerData.CaptureStats(_character.Data.StatIns);
                StatsChanged?.Invoke(_character.Data.StatIns);
            }
            if (_character.CooldownSys.Count > 0)
            {
                SkillCooldownChanged?.Invoke(_character.CooldownSys);
            }
        }

        private bool IsStatsChanged(Stats stats)
        {
            if (stats[ResourceType.Health] != _observerData.BeforeBases[(int)ResourceType.Health])
                return true;
            if (stats[ResourceType.Mana] != _observerData.BeforeBases[(int)ResourceType.Mana])
                return true;
            if (stats[ResourceType.Stamina] != _observerData.BeforeBases[(int)ResourceType.Stamina])
                return true;

            if (stats[StatType.Health] != _observerData.BeforeResources[(int)ResourceType.Health])
                return true;
            if (stats[StatType.Mana] != _observerData.BeforeResources[(int)ResourceType.Mana])
                return true;
            if (stats[StatType.Stamina] != _observerData.BeforeResources[(int)ResourceType.Stamina])
                return true;

            return false;
        }
    }
}
