using Chunity.Framework;
using System;
using UnityEngine;
using Whatever.Entities.Characters;

namespace Whatever.UI
{
    public class EquipmentUIContent : MainMenuUIContent
    {
        public enum SubMenu { Default, Detailed }

        private static readonly string[] _subMenuNameKey = new string[] { "Simple", "Detailed" };

        [SerializeField]
        private GameObject _detailed;
        private Action[] _showSubMenuActions;
        private SubMenu _currentMenu;

        [SerializeField]
        private ImageButtonView[] _equipmentSlots;
        private int _lastSlot = -1;

        [SerializeField]
        private InfiniteHorizontalScroll _itemList;
        private ImageButtonView[] _slots;

        public override string TitleKey => "Equipment";
        public override string[] SubMenuNameKeys => _subMenuNameKey;

        public void Init()
        {
            _itemList.Init();
            _showSubMenuActions = new Action[2];
            _showSubMenuActions[0] = ShowSimple;
            _showSubMenuActions[1] = ShowDetailed;
            for (int i = 0; i < _equipmentSlots.Length; i++)
            {
                int index = i;
                _equipmentSlots[i].Button.onClick.AddListener(() => OnSlotClicked(index));
            }

            _slots = new ImageButtonView[_itemList.Slots.Length];
            for (int i = 0; i < _slots.Length; i++)
            {
                _slots[i] = _itemList.Slots[i].GetComponent<ImageButtonView>();
            }
            _itemList.SlotBound += ChangeItemList;
        }

        public override void ShowSubMenu(int menu)
        {
            SubMenu type = (SubMenu)menu;
            if (_currentMenu != type)
            {
                _currentMenu = type;
                _showSubMenuActions[menu].Invoke();
            }
        }

        public void Refresh(Character character)
        {

        }

        private void OnSlotClicked(int index)
        {
            bool isNewIndex = _lastSlot != index;
            if (isNewIndex == false)
                _lastSlot = -1;
            else
            {
                _lastSlot = index;
                _itemList.Refresh(index * 5);
            }

            _itemList.gameObject.SetActive(isNewIndex);
        }

        private void ChangeItemList(int slotIndex, int itemIndex)
        {
            // TODO : 아이템 이미지 교체
            int step = Mathf.Abs(itemIndex % 50);
            float brightness = Mathf.Lerp(0.3f, 1f, step / 49f);
            _slots[slotIndex].Image.color = Color.black * brightness;
        }

        public override void SetActive(bool value)
        {
            gameObject.SetActive(value);
            if (value == false)
            {
                _itemList.gameObject.SetActive(false);
            }
        }

        private void ShowSimple()
        {
            _detailed.SetActive(false);
        }

        private void ShowDetailed()
        {
            _detailed.SetActive(true);
        }
    }
}
