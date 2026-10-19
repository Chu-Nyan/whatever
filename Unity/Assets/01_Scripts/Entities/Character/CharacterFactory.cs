using Chunity.Core;
using Chunity.Utility;
using UnityEngine;
using Whatever.Core;
using Whatever.Entities.Items;
using Whatever.GameSystems;

namespace Whatever.Entities.Characters
{
    public class CharacterFactory : Singleton<CharacterFactory>
    {
        public const string Asset_Character = "Character";

        private readonly ObjectPooling<Character> _pool;
        private PersistentDataContainer _persistentContainer;

        public CharacterFactory()
        {
            _pool = new(() => AssetManager.InstantiateAssetSync<Character>(Asset_Character));
        }

        public void Init(PersistentDataContainer persistentContainer)
        {
            _persistentContainer = persistentContainer;
        }

        public Character CreateCharacter(int id, Vector3 pos, string key = null)
        {
            if (_persistentContainer.TryGetQuickSlotInventory(key, out var inven) == false)
                inven = QuickSlotInventory.Empty;

            var data = DataBase.Instance.CharacterRepo.DefinitionData[id];
            var actor = _pool.Dequeue();
            actor.Deactivated += Release;
            actor.SetPositionWithNavMash(pos);
            actor.Setup(data, inven);
            var skillSet = DataBase.Instance.SkillRepo.SkillSet[data.SkillSetID];
            if (skillSet != null)
            {
                for (int i = 0; i < skillSet.Length; i++)
                {
                    actor.SkillBook.AddSkill(skillSet[i]);
                }
            }

            actor.SetActive(true);

            return actor;
        }

        public Character CreateCharacter(int id, Vector3 pos)
        {
            return CreateCharacter(id, pos, "Empty");
        }

        private void Release(Character character)
        {
            _pool.Enqueue(character);
        }
    }
}
