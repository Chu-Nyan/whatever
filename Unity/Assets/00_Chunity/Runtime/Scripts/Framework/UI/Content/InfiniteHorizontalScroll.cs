using System;
using UnityEngine;
using UnityEngine.UI;

namespace Chunity.Framework
{
    public class InfiniteHorizontalScroll : MonoBehaviour
    {
        [SerializeField]
        private ScrollRect _scrollRect;
        [SerializeField]
        private RectTransform[] _slots;
        [SerializeField]
        private float _spacing = 5f;
        private int _itemCount;

        private int[] _slotItemIndices;
        private float _slotStride;

        /// <summary>first : slot index, second : item index</summary>
        public event Action<int, int> SlotBound;

        public RectTransform[] Slots => _slots;

        public void Init()
        {
#if UNITY_EDITOR
            if (_scrollRect.content.anchorMin.x != 0f || _scrollRect.content.anchorMax.x != 0f || _scrollRect.content.pivot.x != 0f)
                Debug.LogWarning($"{name}: Content의 anchor.x(min/max)와 pivot.x는 0이어야 합니다.", this);

            if (_slots[0].anchorMin.x != 0f || _slots[0].anchorMax.x != 0f || _slots[0].pivot.x != 0f)
                Debug.LogWarning($"{name}: Slot의 anchor.x(min/max)와 pivot.x는 0이어야 합니다.", this);
#endif

            _slotStride = _slots[0].rect.size.x + _spacing;
            int slotCount = Mathf.CeilToInt(_scrollRect.viewport.rect.width / _slotStride) + 2;
            _slotItemIndices = new int[_slots.Length];
            _scrollRect.onValueChanged.AddListener(_ => UpdateSlots());
        }

        public void Refresh(int count)
        {
            _itemCount = count;
            _scrollRect.content.sizeDelta = new Vector2(count * _slotStride - _spacing, _scrollRect.content.sizeDelta.y);
            _scrollRect.StopMovement();
            _scrollRect.horizontalNormalizedPosition = 0f;
            Array.Fill(_slotItemIndices, -1);

            int activeSlotCount = Math.Min(_slots.Length, count);
            for (int i = 0; i < activeSlotCount; i++)
                _slots[i].gameObject.SetActive(true);
            for (int i = activeSlotCount; i < _slots.Length; i++)
                _slots[i].gameObject.SetActive(false);

            UpdateSlots();
        }

        private void UpdateSlots()
        {
            int slotCount = _slots.Length;
            float scrollOffset = -_scrollRect.content.anchoredPosition.x;
            int firstIndex = Mathf.Clamp(Mathf.FloorToInt(scrollOffset / _slotStride), 0, _itemCount - slotCount);

            for (int i = 0; i < slotCount; i++)
            {
                int itemIndex = firstIndex + ((i - firstIndex) % slotCount + slotCount) % slotCount;

                _slots[i].anchoredPosition = new Vector2(itemIndex * _slotStride, 0f);

                if (_slotItemIndices[i] != itemIndex)
                {
                    _slotItemIndices[i] = itemIndex;
                    SlotBound?.Invoke(i, itemIndex);
                }
            }
        }
    }
}
