using Chunity.Framework;
using UnityEngine;
using Whatever.Entities.Characters;
using Whatever.GameSystems;
using Whatever.GameSystems.Stat;
using Whatever.ObjectControl;
using Whatever.UI;

namespace Whatever.Facade
{
    public class PlayerFacade
    {
        private readonly CharacterFactory _factory;
        private readonly ObjectController _controller;
        private readonly PersistentDataContainer _persistentContainer;
        private readonly HudUI _hudUI;
        private readonly PlayerCharacterObserver _observer;
        private readonly VCamFollow _mainCam;

        private Character _player;
        private QuickSlotLoadout _quickslot;

        public PlayerFacade(CharacterFactory factory, ObjectController controller, HudUI hudUI, PersistentDataContainer persistentContainer, PlayerCharacterObserver observer, VCamFollow mainCam)
        {
            _factory = factory;
            _controller = controller;
            _hudUI = hudUI;
            _persistentContainer = persistentContainer;
            _observer = observer;
            _mainCam = mainCam;
            _hudUI.SetupBtn(OpenInventory, OpenSkillBook, OpenSetting);
        }

        public void InitObserverCallbacks()
        {
            _observer.SkillCooldownChanged += RefreshQuickSlotCooldown;
            _observer.StatsChanged += RefreshHudUIStats;
        }

        public void CreateCharacter(int id, Vector3 position)
        {
            var player = _factory.CreateCharacter(id, position, "player");
            player.SkillBook.AddSkill(1);
            player.SkillBook.AddSkill(2);
            BindPlayer(player);
            _player = player;
        }

        public void FollowCamera()
        {
            _mainCam.SetThirdPersonTarget(_player.transform);
        }

        public void BindPlayer(Character character)
        {
            _quickslot = _persistentContainer.GetQuickSlot("player");
            _controller.ConnectPlayerAgent(character, _quickslot);
            _observer.SetCharacter(character);
            SetupHudUI(character);
        }

        private void OpenInventory()
        {
            Debug.Log($"{_player.Inventory.Count}");
        }

        private void OpenSkillBook()
        {
            Debug.Log($"{_player.SkillBook.Count}");
        }

        private void OpenSetting()
        {
            Debug.Log($"Open Setting");
        }

        private void RefreshHudUIStats(Stats stat)
        {
            _hudUI.Stats.SetHP(stat[ResourceType.Health] / stat[StatType.Health]);
            _hudUI.Stats.SetMP(stat[ResourceType.Mana] / stat[StatType.Mana]);
            _hudUI.Stats.SetStamina(stat[ResourceType.Stamina] / stat[StatType.Stamina]);
        }

        private void SetupHudUI(Character player)
        {
            for (int i = 0; i < 10; i++)
            {
                var id = _quickslot[i + 2];
                if (id == 0)
                    _hudUI.QuickSlot.SetupEmpty(i);
                else if (player.SkillBook.Contains(id) == false)
                    _hudUI.QuickSlot.SetupFalse(i);
                else
                    _hudUI.QuickSlot.SetupSlot(i, null, player.CooldownSys.GetRemainTime(CooldownSystem.Category.Skill, id));
            }

            for (int i = 10; i < 12; i++)
            {
                var id = _quickslot[i + 2];
                if (id == 0)
                    _hudUI.QuickSlot.SetupEmpty(i);
                else if (player.Inventory.TryGetItem(id, out var item) == false)
                    _hudUI.QuickSlot.SetupFalse(i);
                else
                    _hudUI.QuickSlot.SetupSlot(i, null, player.CooldownSys.GetRemainTime(CooldownSystem.Category.Item, id));
            }
        }

        private void RefreshQuickSlotCooldown(CooldownSystem cooldownSys)
        {
            for (int i = 0; i < 10; i++)
            {
                var id = _quickslot[i + 2];
                if (cooldownSys.ContainSkills(id) == false)
                    continue;

                _hudUI.QuickSlot.RefreshCooldown(i, cooldownSys.GetRemainTime(CooldownSystem.Category.Skill, id));
            }

            for (int i = 10; i < 12; i++)
            {
                var id = _quickslot[i + 2];
                if (cooldownSys.ContainItems(id) == false)
                    continue;
                _hudUI.QuickSlot.RefreshCooldown(i, cooldownSys.GetRemainTime(CooldownSystem.Category.Item, id));
            }
        }
    }
}
