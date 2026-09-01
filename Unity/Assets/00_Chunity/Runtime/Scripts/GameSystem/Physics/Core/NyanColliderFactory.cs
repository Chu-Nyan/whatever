using Chunity.Utility;
using UnityEngine;

namespace Chunity.GameSystem.Physics
{
    public class NyanColliderFactory : Singleton<NyanColliderFactory>
    {
        private NyanCollisonSystem _system;

        public void Init(NyanCollisonSystem sys)
        {
            if (_system != null)
            {
                Debug.LogError("Already initialized.");
                return;
            }

            _system = sys;
        }

        public NyanCollider Create(INyanCollisionProvider provider, IShape shape, bool isActive = true, string comment = default)
        {
            NyanCollider collider = new(shape, isActive, comment);
            _system.RegisterEntity(collider, provider);
            return collider;
        }

        public NyanCollider Create(INyanCollisionProvider provider, string comment = default)
        {
            NyanCollider collider = new(new CircleShape(), false, comment);
            _system.RegisterEntity(collider, provider);
            return collider;
        }
    }
}
