using UnityEngine;
using Whatever.Entities.Characters;
using Whatever.GameSystems;
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

        public PlayerFacade(CharacterFactory factory, ObjectController controller, HudUI hudUI, PersistentDataContainer persistentContainer, PlayerCharacterObserver observer)
        {
            _factory = factory;
            _controller = controller;
            _hudUI = hudUI;
            _persistentContainer = persistentContainer;
            _observer = observer;
        }

        public void InitObserverCallbacks()
        {
            _hudUI.InitPlayerObserver(_observer);
        }

        public void CreateCharacter(int id, Vector3 position)
        {
            var player = _factory.CreateCharacter(id, position, "player");
            player.SkillBook.AddSkill(1);
            player.SkillBook.AddSkill(2);
            BindPlayer(player);
        }

        public void BindPlayer(Character character)
        {
            var quickslot = _persistentContainer.GetQuickSlot("player");
            var loadout = _persistentContainer.GetQuickSlot("player");
            _controller.ConnectPlayerAgent(character, quickslot);
            _hudUI.SetupQuickSlot(loadout, character);
            _observer.SetCharacter(character);
        }
    }
}
