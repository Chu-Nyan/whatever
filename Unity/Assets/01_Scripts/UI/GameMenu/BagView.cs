using System;
using TMPro;
using UnityEngine;
using Whatever.Entities.Items;

namespace Whatever.UI
{
    public class BagView : MonoBehaviour
    {
        public TMP_Text Name;
        public ImageButtonView[] ConsumableSlots;
        public ImageButtonView[] InventorySlots;

        public event Action<int> QuickslotClicked;
        public event Action<int> InventoryClicked;

        public void Init()
        {
            for (int i = 0; i < ConsumableSlots.Length; i++)
            {
                var index = i;
                ConsumableSlots[i].Button.onClick.AddListener(() => QuickslotClicked.Invoke(index));
            }

            for (int i = 0; i < InventorySlots.Length; i++)
            {
                var index = i;
                InventorySlots[i].Button.onClick.AddListener(() => InventoryClicked.Invoke(index));
            }
        }

        public void RefreshBag(ItemBase[] quickSlot, ItemBase[] inven)
        {
            for (int i = 0; i < quickSlot.Length; i++)
            {
                if (quickSlot[i] == null)
                    InventorySlots[i].Image.sprite = null;
                else
                    ConsumableSlots[i].Image.sprite = quickSlot[i].Def.Icon;
            }

            for (int i = 0; i < inven.Length; i++)
            {
                if (inven[i] == null)
                    InventorySlots[i].Image.sprite = null;
                else
                    InventorySlots[i].Image.sprite = inven[i].Def.Icon;
            }
        }
    }
}
