using UnityEngine;
using Whatever.Entities.Items;

namespace Whatever.UI
{
    public class InventoryUIContent : MainMenuUIContent
    {
        private static readonly string[] _subMenuNameKeys = new string[] { "Inventory" };

        [SerializeField]
        private CarrierView _carrierView;
        [SerializeField]
        private BagPresetView _bagPresetView;
        [SerializeField]
        private ItemInfoView _itemInfo;

        private CategoryInventory _carrier;
        private QuickSlotInventory[] _characterInven;

        public override string TitleKey => "Inventory";
        public override string[] SubMenuNameKeys => _subMenuNameKeys;

        public void Init()
        {
            _carrierView.Init();
            _characterInven = new QuickSlotInventory[3];
            _carrierView.TapChanged += a => RefreshCarrier((CategoryInventory.Tap)a);
        }

        public void SetCarrier(CategoryInventory inventory)
        {
            _carrier = inventory;
        }

        public void SetCharacterInventory(int index, string name, QuickSlotInventory inven)
        {
            _characterInven[index] = inven;
            _bagPresetView.Bags[index].Name.text = name;
        }

        public void RefreshCarrier(CategoryInventory.Tap tap)
        {
            if (_carrier == null)
                return;

            _carrierView.RefreshCarrier(_carrier[tap].List);
        }

        public void RefreshQuickSlot()
        {
            for (int i = 0; i < 3; i++)
            {
                if (_characterInven[i] == null)
                {
                    _bagPresetView.Bags[i].gameObject.SetActive(false);
                }
                else
                {
                    _bagPresetView.Bags[i].gameObject.SetActive(true);
                    var inven = _characterInven[i];
                    _bagPresetView.Bags[i].RefreshBag(inven.QuickSlot.List, inven.Inventory.List);
                }
            }
        }

        public void Refresh()
        {
            RefreshCarrier((CategoryInventory.Tap)_carrierView.CurrentTap);
            RefreshQuickSlot();
        }

        public override void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public override void ShowSubMenu(int index)
        {

        }
    }
}
