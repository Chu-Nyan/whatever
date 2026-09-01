using Chunity.Core;
using Chunity.Data;
using System;

namespace Chunity.GameSystem.Physics
{
    public class CollisionTrigger : INyanCollisionProvider
    {
        public readonly int ID;
        private readonly NyanCollider _collider;
        private bool _isActive = false;

        private Action<int> _entered;

        public NyanCollider Collider
        {
            get => _collider;
        }

        public CollisionTrigger(string triggerName)
        {
            ID = IDGenerator.Next();
            _collider = NyanColliderFactory.Instance.Create(this, triggerName);
        }

        public void Setup(IShape shape, Pose2D pose2D, bool isActive)
        {
            _collider.SetShape(shape);
            _collider.SetTransform(pose2D);
            SetActive(isActive);
        }

        public void SetActive(bool value)
        {
            if (_isActive == value)
                return;

            _collider.SetActive(value);
            _isActive = value;
        }

        public void RegisterOnEntered(Action<int> action)
        {
            _entered += action;
        }

        public void OnNyanCollisionEnter(INyanCollisionProvider provider)
        {
            _entered.Invoke(ID);
        }

        public void OnNyanCollisionExit(INyanCollisionProvider provider)
        {

        }

    }
}
