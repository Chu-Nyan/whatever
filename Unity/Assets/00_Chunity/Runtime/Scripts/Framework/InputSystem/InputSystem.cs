using Chunity.Utility;
using UnityEngine;

namespace Chunity.Framework
{
    public class InputSystem : Singleton<InputSystem>
    {
        private readonly InputSystem_Actions _action;
        public readonly InputEvent<Vector2> MousePoint;
        public readonly InputEvent<Vector2> WASD;
        public readonly InputEvent LeftClick;
        public readonly FixedInputEvent<int>[] Skills;

        private Vector2 _mousePosition;

        public Vector2 MousePosition
        {
            get => _mousePosition;
        }

        public InputSystem()
        {
            _action = new();
            MousePoint = new(_action.Player.MousePosition, InputEventType.Performed);
            MousePoint.RegisterPerformed(value => _mousePosition = value);
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
