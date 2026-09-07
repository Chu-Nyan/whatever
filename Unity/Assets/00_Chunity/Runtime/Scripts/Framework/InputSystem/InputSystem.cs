using Chunity.Utility;

namespace Chunity.Framework
{
    public class InputSystem : Singleton<InputSystem>
    {
        private readonly InputSystem_Actions _action;

        public readonly AlwaysActionMap Always;
        public readonly PlayerActionMap Player;

        public InputSystem()
        {
            _action = new();
            Always = new(_action.Always);
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
