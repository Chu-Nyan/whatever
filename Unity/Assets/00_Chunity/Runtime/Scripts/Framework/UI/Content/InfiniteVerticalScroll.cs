using System;
using UnityEngine;
using UnityEngine.UI;

namespace Chunity.Framework
{
    public sealed class InfiniteVerticalScroll : MonoBehaviour
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
        private int _activeCount;

        public event Action<int, int> SlotBound;

        public RectTransform[] Slots => _slots;

        public void Init()
        {
#if UNITY_EDITOR
            if (_scrollRect.content.anchorMin.y != 1f || _scrollRect.content.anchorMax.y != 1f || _scrollRect.content.pivot.y != 1f)
                Debug.LogWarning($"{name}: Content의 anchor.y(min/max)와 pivot.y는 1이어야 합니다.", this);

            if (_slots[0].anchorMin.y != 1f || _slots[0].anchorMax.y != 1f || _slots[0].pivot.y != 1f)
                Debug.LogWarning($"{name}: Slot의 anchor.y(min/max)와 pivot.y는 1이어야 합니다.", this);
#endif

            _slotStride = _slots[0].rect.size.y + _spacing;
            _slotItemIndices = new int[_slots.Length];

            _scrollRect.onValueChanged.AddListener(_ => UpdateSlots());
        }

        public int GetItemIndex(int slotIndex)
        {
            return _slotItemIndices[slotIndex];
        }

        public void Refresh(int count)
        {
            _itemCount = count;
            _scrollRect.content.sizeDelta = new Vector2(_scrollRect.content.sizeDelta.x, count * _slotStride - _spacing);
            _scrollRect.StopMovement();
            _scrollRect.verticalNormalizedPosition = 1f;
            Array.Fill(_slotItemIndices, -1);

            _activeCount = Math.Min(_slots.Length, count);
            for (int i = 0; i < _activeCount; i++)
                _slots[i].gameObject.SetActive(true);
            for (int i = _activeCount; i < _slots.Length; i++)
                _slots[i].gameObject.SetActive(false);

            UpdateSlots();
        }

        private void UpdateSlots()
        {
            float scrollOffset = _scrollRect.content.anchoredPosition.y;
            int firstIndex = Mathf.Clamp(Mathf.FloorToInt(scrollOffset / _slotStride), 0, _itemCount - _activeCount);

            for (int i = 0; i < _activeCount; i++)
            {
                int itemIndex = firstIndex + ((i - firstIndex) % _activeCount + _activeCount) % _activeCount;

                _slots[i].anchoredPosition = new Vector2(_slots[i].anchoredPosition.x, -itemIndex * _slotStride);

                if (_slotItemIndices[i] != itemIndex)
                {
                    _slotItemIndices[i] = itemIndex;
                    SlotBound?.Invoke(i, itemIndex);
                }
            }
        }
    }
}
