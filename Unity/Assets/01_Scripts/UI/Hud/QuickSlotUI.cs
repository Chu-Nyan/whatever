using UnityEngine;

namespace Whatever.UI
{
    public class QuickSlotUIContent : MonoBehaviour
    {
        [SerializeField]
        private TextImageViewer[] _skillSlots;
        [SerializeField]
        private TextImageViewer[] _itemSlots;

        public void SetupSkillSlot(int index, Sprite sprite, float cooldown)
        {
            _skillSlots[index].Image.sprite = sprite;
            RefreshSkillCooldown(index, cooldown);
        }

        public void SetupItemSlot(int index, Sprite sprite, float cooldown)
        {
            _itemSlots[index].Image.sprite = sprite;
            RefreshItemCooldown(index, cooldown);
        }

        public void SetupEmpty(int index)
        {
            _skillSlots[index].Text.text = "empty";
        }

        public void SetupEmptyItem(int index)
        {
            _itemSlots[index].Text.text = "empty";
        }

        public void SetupFalse(int index)
        {
            _skillSlots[index].Text.text = "false";
        }

        public void SetupFalseItem(int index)
        {
            _itemSlots[index].Text.text = "empty";
        }

        public void RefreshSkillCooldown(int index, float cooldown)
        {
            string text;
            if (cooldown <= 0f)
                text = string.Empty;
            else if (cooldown >= 10f)
                text = cooldown.ToString("F0");
            else
                text = cooldown.ToString("F1");

            _skillSlots[index].Text.text = text;
        }

        public void RefreshItemCooldown(int index, float cooldown)
        {
            string text;
            if (cooldown <= 0f)
                text = string.Empty;
            else if (cooldown >= 10f)
                text = cooldown.ToString("F0");
            else
                text = cooldown.ToString("F1");

            _itemSlots[index].Text.text = text;
        }
    }
}
