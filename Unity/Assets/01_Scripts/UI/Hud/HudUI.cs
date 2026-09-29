using Chunity.Framework;
using UnityEngine;
using Whatever.GameSystems.Stat;

namespace Whatever.UI
{
    public class HudUI : UIController
    {
        [SerializeField]
        private StatsViewer _stats;
        private Stats _statTarget;

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Setup(Stats statTarget)
        {
            _statTarget = statTarget;
            Refresh();
        }

        private void Refresh()
        {
            _stats.SetHP(_statTarget[ResourceType.Health] / _statTarget[StatType.Health]);
            _stats.SetMP(_statTarget[ResourceType.Mana] / _statTarget[StatType.Mana]);
            _stats.SetStamina(_statTarget[ResourceType.Stamina] / _statTarget[StatType.Stamina]);
        }
    }
}
