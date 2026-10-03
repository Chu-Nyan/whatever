using Chunity.Core;
using Chunity.Framework;
using Unity.VisualScripting;
using UnityEngine;
using Whatever.Entities.Characters;
using Whatever.Entities.Items;
using Whatever.Entities.Skill;
using Whatever.GameSystems;
using Whatever.GameSystems.Stat;
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

            _persistentContainer.SaveInventory("player", inven);
        }

        private void CreateFacade()
        {
        }

        private void InitFacade()
        {
        }

        private void GameStart()
        {
            CreatePlayer();


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

        private void CreatePlayer()
        {
            var actor = CharacterFactory.Instance.CreateCharacter(1, Vector3.zero, "player");
            _controller.ConnectPlayerAgent(actor);
            actor.Data.StatIns.AddResource(ResourceType.Health, -10);
            actor.Data.StatIns.AddResource(ResourceType.Mana, -10);
            actor.Data.StatIns.AddResource(ResourceType.Stamina, -10);
        }
    }
}
