using UnityEngine;

namespace Whatever.UI
{
    public class StatsViewer : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _hp;
        [SerializeField]
        private RectTransform _mp;
        [SerializeField]
        private RectTransform _stamina;

        public void SetHP(float scale)
        {
            _hp.localScale = new(scale, 1, 1);
        }

        public void SetMP(float scale)
        {
            _mp.localScale = new(scale, 1, 1);
        }

        public void SetStamina(float scale)
        {
            _stamina.localScale = new(scale, 1, 1);
        }
    }
}
