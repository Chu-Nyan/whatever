using UnityEngine;

namespace Whatever.UI
{
    public class QuickSlotUIContent : MonoBehaviour
    {
        [SerializeField]
        private TextImageViewer[] _slots;

        public TextImageViewer this[int index] => _slots[index];

        public void SetupSlot(int index, Sprite sprite, float cooldown)
        {
            _slots[index].Image.sprite = sprite;
            RefreshCooldown(index, cooldown);
        }

        public void SetupEmpty(int index)
        {
            _slots[index].Text.text = "empty";
        }

        public void SetupFalse(int index)
        {
            _slots[index].Text.text = "false";
        }

        public void RefreshCooldown(int index, float cooldown)
        {
            string text;
            if (cooldown <= 0f)
                text = string.Empty;
            else if (cooldown >= 10f)
                text = cooldown.ToString("F0");
            else
            {
                text = cooldown.ToString("F1");
            }


            _slots[index].Text.text = text;
        }
    }
}
