using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Whatever.Entities.Items;

namespace Whatever.UI
{
    public class CarrierView : MonoBehaviour
    {
        [SerializeField]
        private Button[] _categoryTaps;
        [SerializeField]
        private Button _setting;
        [SerializeField]
        private TMP_Text _silver;
        [SerializeField]
        private ImageButtonView[] _slots;

        public int CurrentTap;

        public event Action<int> TapChanged;
        public event Action<int> SlotClicked;

        public void Init()
        {
            for (int i = 0; i < _categoryTaps.Length; i++)
            {
                var index = i;
                _categoryTaps[i].onClick.AddListener(() => OnTapChanged(index));
            }
            for (int i = 0; i < _slots.Length; i++)
            {
                var index = i;
                _slots[i].Button.onClick.AddListener(() => OnSlotClicked(index));
            }
            _setting.onClick.AddListener(() => ShowSetting());
        }

        private void ShowSetting()
        {
            Debug.Log("Carrier setting clicked.");
        }

        public void RefreshCarrier(ItemBase[] inven)
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                if (inven[i] != null)
                    _slots[i].Button.image.sprite = inven[i].Def.Icon;
                else
                    _slots[i].Button.image.sprite = null;
            }
        }

        private void OnTapChanged(int index)
        {
            CurrentTap = index;
            TapChanged?.Invoke(index);
        }

        private void OnSlotClicked(int index)
        {
            SlotClicked?.Invoke(index);
        }
    }
}
