using Chunity.Framework;
using System.Collections.Generic;
using UnityEngine;
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

        public void SetupQuickSlot(QuickSlotLoadout loadout, SkillBook book)
        {
            _quickslotBySkillID.Clear();
            var startIndex = (int)QuickSlotLoadout.SkillSlot.Dodge;
            for (int i = 0; i < 10; i++)
            {
                var skillID = loadout[(QuickSlotLoadout.SkillSlot)startIndex + i];
                if (skillID == 0)
                {
                    _quickSlot.SetupEmpty(i);
                }
                else if (book.Contains(skillID) == false)
                {
                    _quickSlot.SetupFalse(i);
                }
                else
                {
                    _quickSlot.SetupSkillSlot(i, null, book[skillID].RemainingCooldown);
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

        public void RefreshSkillSlots(SkillBook book)
        {
            var used = book.Used;
            for (int i = 0; i < used.Count; i++)
            {
                if (_quickslotBySkillID.TryGetValue(used[i].ID, out var index) == false)
                    continue;

                _quickSlot.RefreshSkillCooldown(index, used[i].RemainingCooldown);
            }
        }
    }
}
