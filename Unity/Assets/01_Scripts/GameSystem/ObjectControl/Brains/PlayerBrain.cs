using Chunity.Framework;
using UnityEngine;

namespace Whatever.ObjectControl
{
    public class PlayerBrain : IBrainStrategy
    {
        private IMovementReceiver _movementReceiver;
        private IOffenseReceiver _combatReceiver;
        private Vector2 _direction;
        private bool _isMoving;

        public BrainType BrainType
        {
            get => BrainType.Player;
        }

        public void Tick()
        {
            MoveDirection();
        }

        public void ControlReceiver(IInputReceiver obj)
        {
            if (obj is IMovementReceiver movement)
                _movementReceiver = movement;
            if (obj is IOffenseReceiver offense)
                _combatReceiver = offense;
        }

        public void Enable()
        {
            InputSystem.Instance.WASD.RegisterPerformed(SetDiection);
            InputSystem.Instance.WASD.RegisterCanceled(SetDiection);
        }

        public void Disable()
        {
            InputSystem.Instance.WASD.UnregisterPerformed(SetDiection);
            InputSystem.Instance.WASD.UnregisterCanceled(SetDiection);
            SetDiection(Vector2.zero);
        }

        private void SetDiection(Vector2 dir)
        {
            _direction = dir;
            _isMoving = _direction != Vector2.zero;
        }

        private void MoveDirection()
        {
            if (_isMoving == false)
                return;

            var direction = new Vector3(_direction.x * 0.5f, 0, _direction.y * 0.5f) + _movementReceiver.transform.position;
            _movementReceiver.SetDestination(direction);
        }
    }
}
