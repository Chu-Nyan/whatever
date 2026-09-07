using Chunity.Utility;
using UnityEngine;

namespace Chunity.Framework
{
    public class InputSystem : Singleton<InputSystem>
    {
        private readonly InputSystem_Actions _action;
        public readonly InputEvent<Vector2> MousePoint;
        private Vector2 _mousePosition;

        public readonly PlayerActionMap Player;

        public Vector2 MousePosition
        {
            get => _mousePosition;
        }

        public InputSystem()
        {
            _action = new();
            MousePoint = new(_action.Player.MousePosition, InputEventType.Performed);
            MousePoint.RegisterPerformed(value => _mousePosition = value);

            Player = new(_action.Player);
        }

        public void SetActive(bool value)
        {
            if (value == true)
                _action.Enable();
            else
                _action.Disable();
        }
    }
}
