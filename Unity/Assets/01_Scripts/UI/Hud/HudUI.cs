using Chunity.Framework;
using UnityEngine;
using Whatever.GameSystems;
using Whatever.GameSystems.Stat;

namespace Whatever.UI
{
    public class HudUI : UIController
    {
        [SerializeField]
        private StatsViewer _stats;
        public QuickSlotUIContent QuickSlot;

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
            observer.StatsChanged += RefreshStats;
        }

        public void RefreshStats(Stats stat)
        {
            _stats.SetHP(stat[ResourceType.Health] / stat[StatType.Health]);
            _stats.SetMP(stat[ResourceType.Mana] / stat[StatType.Mana]);
            _stats.SetStamina(stat[ResourceType.Stamina] / stat[StatType.Stamina]);
        }
    }
}
