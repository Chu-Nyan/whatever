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
        private VCamFollow _topviewCam;
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
            _topviewCam = CameraFactory.Instance.CreateFollow(new(Quaternion.Euler(25f, 0f, 0f), 50, new(0, 12, -8f), new(0, 0, 0), transform, transform));
        }

        private void InitStatic()
        {
            CharacterFactory.Instance.Init(_persistentContainer);
        }

        private void InitInstance()
        {
            var inven = new QuickSlotInventory(2, 8);
            inven.Inventory.TryAdd(ItemFactory.Instance.Create(1, 1));
            inven.Inventory.TryAdd(ItemFactory.Instance.Create(2, 1));
            inven.QuickSlot.TryAdd(ItemFactory.Instance.Create(3, 10));
            inven.QuickSlot.TryAdd(ItemFactory.Instance.Create(4, 10));

            var loadout = new QuickSlotLoadout();
            loadout.CopySlot(new int[] { 0, 0, 1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 3, 4 });

            _persistentContainer.SaveQuickSlotInventory("player", inven);
            _persistentContainer.SaveQuickSlotLoadout("player", loadout);
        }

        private void CreateFacade()
        {
            _playerFacade = new(CharacterFactory.Instance, _controller, UIManager.Instance.GetUI<HudUI>(), _persistentContainer, _characterObserver, _topviewCam);
        }

        private void InitFacade()
        {
            _playerFacade.InitObserverCallbacks();
        }

        private void GameStart()
        {
            _playerFacade.CreateCharacter(1, new Vector3(5, 0, 5));
            _playerFacade.FollowCamera();
            for (int i = 0; i < 4; i++)
            {
                CharacterFactory.Instance.CreateCharacter(2, new Vector3(3 + (i * 2), 0, 8));
                CharacterFactory.Instance.CreateCharacter(3, new Vector3(3 + (i * 2), 0, 2));
            }

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
