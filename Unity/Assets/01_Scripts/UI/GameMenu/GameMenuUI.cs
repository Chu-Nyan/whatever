using Chunity.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Whatever.Entities.Characters;
using Whatever.Entities.Items;

namespace Whatever.UI
{
    public class GameMenuUI : UIController
    {
        public enum MenuType { Equipment, Inventory, Skills, Quest, Setting }

        [SerializeField]
        private Button[] _mainMenuBtns;
        [SerializeField]
        private TextButtonView[] _subMenuBtns;
        [SerializeField]
        private TMP_Text _title;
        [SerializeField]
        private Button _exit;

        [SerializeField]
        private EquipmentUIContent _equipmentUI;
        [SerializeField]
        private InventoryUIContent _inventoryContent;
        [SerializeField]
        private QuestUIContent _questUI;
        [SerializeField]
        private SettingUIContent _settingUI;

        private MainMenuUIContent _currentMenu;
        private Character _target;

        private void Awake()
        {
            _mainMenuBtns[(int)MenuType.Equipment].onClick.AddListener(() => ShowEquipment());
            _mainMenuBtns[(int)MenuType.Inventory].onClick.AddListener(() => ShowInventory());
            _mainMenuBtns[(int)MenuType.Quest].onClick.AddListener(() => ShowQuest());
            _mainMenuBtns[(int)MenuType.Setting].onClick.AddListener(() => ShowSetting());

            for (int i = 0; i < _subMenuBtns.Length; i++)
            {
                int index = i;
                _subMenuBtns[i].Button.onClick.AddListener(() => _currentMenu.ShowSubMenu(index));
            }
            _exit.onClick.AddListener(() => Hide());

            _equipmentUI.Init();
            _questUI.Init();
            _inventoryContent.Init();
            _currentMenu = _questUI;
            ShowSetting();
        }

        public void SetPlayerCharacter(Character target)
        {
            _target = target;
            SetCharacterInventory(0, target.Data.Def.NameKey, target.Inventory);
        }

        public void SetPlayerCarrier(CategoryInventory inventory)
        {
            _inventoryContent.SetCarrier(inventory);
        }

        public void SetCharacterInventory(int order, string name, QuickSlotInventory inven)
        {
            _inventoryContent.SetCharacterInventory(order, name, inven);
        }

        public void SetQuestBook(int book)
        {
            _questUI.SetQuestBook(book);
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public void ShowEquipment()
        {
            ShowMainMenu(_equipmentUI);
            _equipmentUI.Refresh(_target);
        }

        public void ShowInventory()
        {
            ShowMainMenu(_inventoryContent);
            _inventoryContent.Refresh();
        }

        public void ShowQuest()
        {
            ShowMainMenu(_questUI);
        }

        public void ShowSetting()
        {
            ShowMainMenu(_settingUI);
        }

        private void ShowMainMenu(MainMenuUIContent menu)
        {
            if (_currentMenu == menu)
                return;

            _currentMenu.gameObject.SetActive(false);
            RefreshMenu(menu.TitleKey, menu.SubMenuNameKeys);
            menu.SetActive(true);
            _currentMenu = menu;
        }

        private void RefreshMenu(string title, string[] subMenus)
        {
            _title.text = title;

            for (int i = 0; i < subMenus.Length; i++)
            {
                _subMenuBtns[i].Text.text = subMenus[i];
                _subMenuBtns[i].gameObject.SetActive(true);
            }

            for (int i = subMenus.Length; i < _subMenuBtns.Length; i++)
            {
                _subMenuBtns[i].gameObject.SetActive(false);
            }
        }
    }
}
