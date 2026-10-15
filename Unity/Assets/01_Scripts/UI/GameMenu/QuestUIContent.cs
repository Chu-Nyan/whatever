using Chunity.Framework;
using UnityEngine;

namespace Whatever.UI
{
    public class QuestUIContent : MainMenuUIContent
    {
        private static readonly string[] _subMenuNameKeys = new string[] { };
        [SerializeField]
        private InfiniteVerticalScroll _questList;
        private TextButtonView[] _slots;

        [SerializeField]
        private QuestUIInfoView _infoView;

        public override string TitleKey => "Quest";
        public override string[] SubMenuNameKeys => _subMenuNameKeys;

        public void Init()
        {
            _questList.Init();
            _questList.SlotBound += RefreshSlot;
            _slots = new TextButtonView[_questList.Slots.Length];
            for (int i = 0; i < _questList.Slots.Length; i++)
            {
                _slots[i] = _questList.Slots[i].GetComponent<TextButtonView>();
                int index = i;
                _slots[i].Button.onClick.AddListener(() => RefreshQuestInfo(index));
            }
        }

        public void SetQuestBook(int book)
        {
            // TODO : 퀘스트 연결
            _questList.Refresh(book);
        }

        public override void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public override void ShowSubMenu(int index)
        {

        }

        private void RefreshSlot(int slotIndex, int itemIndex)
        {
            _slots[slotIndex].Text.text = itemIndex.ToString();
        }

        private void RefreshQuestInfo(int slotIndex)
        {
            var itemIndex = _questList.GetItemIndex(slotIndex);
            _infoView.Refresh(null, null, itemIndex.ToString(), slotIndex.ToString(), itemIndex.ToString(), slotIndex.ToString());
        }
    }
}
