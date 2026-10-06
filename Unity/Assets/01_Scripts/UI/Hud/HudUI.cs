using Chunity.Framework;
using System.Collections.Generic;
using UnityEngine;
using Whatever.GameSystems;
using Whatever.GameSystems.Combat;
using Whatever.GameSystems.Stat;
using Whatever.ObjectControl;

namespace Whatever.UI
{
    public class HudUI : UIController
    {
        [SerializeField]
        private StatsViewer _stats;
        [SerializeField]
        private QuickSlotUIContent _quickSlot;
        private Dictionary<int, int> _quickslotBySkillID;

        public HudUI()
        {
            _quickslotBySkillID = new(10);
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public void InitPlayerObserver(PlayerCharacterObserver observer)
        {
            //observer.CharacterChanged +=  
            observer.StatsChanged += RefreshStats;
            observer.SkillCooldownChanged += RefreshSkillSlots;
        }

        public void SetupQuickSlot(QuickSlotLoadout loadout, IOffenseReceiver offense)
        {
            _quickslotBySkillID.Clear();
            var cooldownSys = offense.CooldownSys;
            var skillBook = offense.SkillBook;

            var startIndex = (int)QuickSlotLoadout.SkillSlot.Dodge;
            for (int i = 0; i < 10; i++)
            {
                var skillID = loadout[(QuickSlotLoadout.SkillSlot)startIndex + i];
                if (skillID == 0)
                {
                    _quickSlot.SetupEmpty(i);
                }
                else if (skillBook.Contains(skillID) == false)
                {
                    _quickSlot.SetupFalse(i);
                }
                else
                {
                    _quickSlot.SetupSkillSlot(i, null, cooldownSys.GetRemainTime(CooldownSystem.Category.Skill, skillID));
                    _quickslotBySkillID[skillID] = i;
                }
            }
        }

        public void RefreshStats(Stats stat)
        {
            _stats.SetHP(stat[ResourceType.Health] / stat[StatType.Health]);
            _stats.SetMP(stat[ResourceType.Mana] / stat[StatType.Mana]);
            _stats.SetStamina(stat[ResourceType.Stamina] / stat[StatType.Stamina]);
        }

        public void RefreshSkillSlots(CooldownSystem sys)
        {
            foreach (var item in _quickslotBySkillID)
            {
                if (sys.ContainSkills(item.Key) == false)
                    continue;

                float remain = sys.GetRemainTime(CooldownSystem.Category.Skill, item.Key);
                _quickSlot.RefreshSkillCooldown(item.Value, remain);
            }
        }
    }
}
