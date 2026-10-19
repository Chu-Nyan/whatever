using System;
using UnityEngine;
using UnityEngine.UI;
using Whatever.Entities.Characters;

namespace Whatever.UI
{
    public class InventoryUIContent : MainMenuUIContent
    {
        private static readonly string[] _subMenuNameKeys = new string[] { "Inventory" };

        public CarrierContent _carrier;
        public BagContent[] _bags;
        public ItemInfoContent _itemInfo;
        [SerializeField]
        private Button _reloadBag;
        [SerializeField]
        private Button _savePreset;
        [SerializeField]
        private Button[] _presets;

        public override string TitleKey => "Inventory";
        public override string[] SubMenuNameKeys => _subMenuNameKeys;

        public event Action<int> PresetSlotClicked;
        public event Action PresetReloaded;
        public event Action PresetSaved;

        public void Init()
        {
            _carrier.Init();
        }

        public void Refresh(Character character)
        {
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
