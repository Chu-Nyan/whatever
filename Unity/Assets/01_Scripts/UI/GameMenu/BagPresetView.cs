using System;
using UnityEngine;
using UnityEngine.UI;

namespace Whatever.UI
{
    public class BagPresetView : MonoBehaviour
    {
        public BagView[] Bags;
        [SerializeField]
        private Button _reloadBag;
        [SerializeField]
        private Button _savePreset;
        [SerializeField]
        private Button[] _presets;

        public event Action<int> PresetSlotClicked;
        public event Action PresetReloaded;
        public event Action PresetSaved;

        public void RegisterSlotCallback(Action<int> onQuickSlotClicked, Action<int> onInvenSlotClicked)
        {
            for (int i = 0; i < Bags.Length; i++)
            {

                Bags[i].QuickslotClicked += onQuickSlotClicked;
                Bags[i].InventoryClicked += onInvenSlotClicked;
            }
        }
    }
}
