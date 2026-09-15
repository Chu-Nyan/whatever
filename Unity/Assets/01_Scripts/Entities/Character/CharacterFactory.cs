using Chunity.Core;
using Chunity.Utility;
using UnityEngine;
using Whatever.Entities.Items;
using Whatever.GameSystems;

namespace Whatever.Entities.Characters
{
    public class CharacterFactory : Singleton<CharacterFactory>
    {
        public const string Asset_Character = "Character";

        private readonly ObjectPooling<Character> _pool;
        private LoadoutContainer _loadoutContainer;

        public CharacterFactory()
        {
            _pool = new(() => AssetManager.InstantiateAssetSync<Character>(Asset_Character));
        }

        public void Init(LoadoutContainer loadoutContainer)
        {
            _loadoutContainer = loadoutContainer;
        }

        public Character CreateCharacter(Vector3 pos, string key)
        {
            if (_loadoutContainer.TryGetInventory(key, out var inven) == false)
            {
                inven = Inventory.Empty;
            }

            var actor = _pool.Dequeue();
            actor.RegisterDeactivated(_pool.Enqueue);
            actor.SetPositionWithNavMash(pos);
            actor.Setup(inven);
            actor.SetActive(true);

            return actor;
        }

        public Character CreateCharacter(Vector3 pos)
        {
            return CreateCharacter(pos, "Empty");
        }
    }
}
