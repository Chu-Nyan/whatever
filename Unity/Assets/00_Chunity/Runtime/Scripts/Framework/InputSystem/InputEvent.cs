using System;
using UnityEngine.InputSystem;

namespace Chunity.Framework
{
    public class InputEvent
    {
        private event Action Performed;
        private event Action Canceled;

        public InputEvent(InputAction action, InputEventType type)
        {
            if ((type & InputEventType.Performed) != 0)
                action.performed += InvokePerformed;

            if ((type & InputEventType.Canceled) != 0)
                action.canceled += InvokeCanceled;
        }

        private void InvokePerformed(InputAction.CallbackContext value)
        {
            Performed?.Invoke();
        }

        private void InvokeCanceled(InputAction.CallbackContext value)
        {
            Canceled?.Invoke();
        }

        public void RegisterPerformed(Action action)
        {
            Performed += action;
        }

        public void RegisterCanceled(Action action)
        {
            Canceled += action;
        }

        public void UnregisterPerformed(Action action)
        {
            Performed -= action;
        }

        public void UnregisterCanceled(Action action)
        {
            Canceled -= action;
        }
    }
}
