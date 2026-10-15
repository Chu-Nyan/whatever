using Chunity.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Whatever.Entities.Characters;

namespace Whatever.UI
{
    public class GameMenuUI : UIController
    {
        public enum MenuType { Equipment, Inventory, Skills, Setting }

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

        private MainMenuUIContent _currentMenu;
        private Character _target;

        private void Awake()
        {
            _exit.onClick.AddListener(() => Hide());
            _mainMenuBtns[0].onClick.AddListener(() => ShowEquipment());
            for (int i = 0; i < _subMenuBtns.Length; i++)
            {
                int index = i;
                _subMenuBtns[i].Button.onClick.AddListener(() => _currentMenu.ShowSubMenu(index));
            }

            _equipmentUI.Init();
        }

        public void SetTargetCharacter(Character target)
        {
            _target = target;
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

        private void ShowMainMenu(MainMenuUIContent menu)
        {
            if (_currentMenu == menu)
                return;

            //_currentMenu.gameObject.SetActive(false);
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
