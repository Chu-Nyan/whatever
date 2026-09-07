using UnityEngine;

namespace Chunity.Framework
{
    public class AlwaysActionMap
    {
        public readonly InputEvent<Vector2> MousePoint;

        private Vector2 _mousePosition;

        public Vector2 MousePosition
        {
            get => _mousePosition;
        }

        public AlwaysActionMap(InputSystem_Actions.AlwaysActions actions)
        {
            MousePoint = new(actions.MousePosition, InputEventType.Performed);
            MousePoint.RegisterPerformed(value => { _mousePosition = value; });
        }
    }
}
