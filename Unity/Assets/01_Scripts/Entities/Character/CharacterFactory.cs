using Chunity.Core;
using Chunity.Utility;
using UnityEngine;

namespace Whatever.Entities.Characters
{
    public class CharacterFactory : Singleton<CharacterFactory>
    {
        public const string Asset_Character = "Character";

        private readonly ObjectPooling<Character> _pool;

        public CharacterFactory()
        {
            _pool = new(() => AssetManager.InstantiateAssetSync<Character>(Asset_Character));
        }

        public Character CreateCharacter(Vector3 pos)
        {
            var actor = _pool.Dequeue();
            actor.RegisterDeactivated(_pool.Enqueue);
            actor.SetPositionWithNavMash(pos);
            actor.SetActive(true);

            return actor;
        }
    }
}
