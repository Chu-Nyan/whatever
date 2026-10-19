using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Whatever.UI
{
    public class CarrierContent : MonoBehaviour
    {
        public enum TapCategory { Bag, Remnant, Material, Misc }

        [SerializeField]
        private Button[] _categoryTaps;
        [SerializeField]
        private Button _setting;
        [SerializeField]
        private TMP_Text _silver;
        [SerializeField]
        private ImageButtonView[] _slots;

        public event Action<TapCategory> TapChanged;

        public ImageButtonView this[int slotIndex] => _slots[slotIndex];

        public void Init()
        {
            for (int i = 0; i < _categoryTaps.Length; i++)
            {
                var category = (TapCategory)i;
                _categoryTaps[i].onClick.AddListener(() => OnTapChanged(category));
            }
            _setting.onClick.AddListener(() => ShowSetting());
        }

        public void SetSliver(int value)
        {
            _silver.text = value.ToString();
        }

        private void ShowSetting()
        {
            Debug.Log("Carrier setting clicked.");
        }

        private void OnTapChanged(TapCategory category)
        {
            TapChanged?.Invoke(category);
        }
    }
}
