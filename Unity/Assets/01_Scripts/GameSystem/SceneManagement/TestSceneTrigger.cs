using Chunity.Core;
using Chunity.Framework;
using Unity.VisualScripting;
using UnityEngine;
using Whatever.Entities.Characters;
using Whatever.Entities.Items;
using Whatever.Entities.Skill;
using Whatever.Facade;
using Whatever.GameSystems;
using Whatever.ObjectControl;
using Whatever.UI;

namespace Whatever.Core
{
    [DefaultExecutionOrder(1)]
    public class TestSceneTrigger : MonoBehaviour
    {
        [SerializeField]
        private EngineSetting _engineSetting;
        private ObjectController _controller;
        private PersistentDataContainer _persistentContainer;
        private PlayerCharacterObserver _characterObserver;

        private PlayerFacade _playerFacade;

        private void Awake()
        {
            ChuEngine.Run(_engineSetting, transform.parent);
            CreateStatic();
            CreateInstance();

            InitStatic();
            InitInstance();

            CreateFacade();
            InitFacade();

            GameStart();
        }

        private void CreateStatic()
        {
            new DataBase();
            new SkillFactory();
            new CharacterFactory();
            new ItemFactory();
        }

        private void CreateInstance()
        {
            _controller = transform.AddComponent<ObjectController>();
            _persistentContainer = new();
            _characterObserver = transform.AddComponent<PlayerCharacterObserver>();
        }

        private void InitStatic()
        {
            CharacterFactory.Instance.Init(_persistentContainer);
        }

        private void InitInstance()
        {
            var inven = new Inventory(80);
            inven.TryAdd(ItemFactory.Instance.Create(1, 1));
            inven.TryAdd(ItemFactory.Instance.Create(2, 1));
            inven.TryAdd(ItemFactory.Instance.Create(3, 1));
            inven.TryAdd(ItemFactory.Instance.Create(4, 1));

            var loadout = new QuickSlotLoadout();
            loadout.CopySkillSlot(new int[] { 0, 0, 0, 0, 1, 2, 0, 0, 0, 0, 0, 0 });
            loadout.CopyItemSlot(new int[] { 1, 2 });

            _persistentContainer.SaveInventory("player", inven);
            _persistentContainer.SaveQuickSlot("player", loadout);
        }

        private void CreateFacade()
        {
            _playerFacade = new(CharacterFactory.Instance, _controller, UIManager.Instance.GetUI<HudUI>(), _persistentContainer, _characterObserver);
        }

        private void InitFacade()
        {
            _playerFacade.InitObserverCallbacks();
        }

        private void GameStart()
        {
            _playerFacade.CreateCharacter(1, Vector3.zero);

            //var Stats = new Stats();
            //Stats.SetupBase(DataBase.Instance.CharacterRepo.DefinitionData[2].Stats);
            //Stats.AddResource(ResourceType.Health, +1);
            //Stats.AddResource(ResourceType.Health, -1);
            //Stats.AddResource(ResourceType.Mana, +1);
            //Stats.AddResource(ResourceType.Mana, -1);
            //Stats.AddResource(ResourceType.Stamina, -1);
            //Stats.AddResource(ResourceType.Stamina, +1);
            //var midifier = new StatModifier();
            //midifier.Stat = StatType.Health;
            //midifier.Value = 10;
            //Stats.ApplyModifier(midifier);

        }
    }

}
