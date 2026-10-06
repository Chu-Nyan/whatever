using Chunity.Framework;

namespace Whatever.UI
{
    public class HudUI : UIController
    {
        public StatsViewer Stats;
        public QuickSlotUIContent QuickSlot;

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
