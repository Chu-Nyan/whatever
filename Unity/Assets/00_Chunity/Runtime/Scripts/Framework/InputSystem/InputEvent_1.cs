using System;
using UnityEngine.InputSystem;

namespace Chunity.Framework
{
    public class InputEvent<T> where T : struct
    {
        private event Action<T> Performed;
        private event Action<T> Canceled;

        public InputEvent(InputAction action, InputEventType type)
        {
            if ((type & InputEventType.Performed) != 0)
                action.performed += InvokePerformed;

            if ((type & InputEventType.Canceled) != 0)
                action.canceled += InvokeCanceled;
        }

        private void InvokePerformed(InputAction.CallbackContext value)
        {
            Performed?.Invoke(value.ReadValue<T>());
        }

        private void InvokeCanceled(InputAction.CallbackContext value)
        {
            Canceled?.Invoke(value.ReadValue<T>());
        }

        public void RegisterPerformed(Action<T> action)
        {
            Performed += action;
        }

        public void RegisterCanceled(Action<T> action)
        {
            Canceled += action;
        }

        public void UnregisterPerformed(Action<T> action)
        {
            Performed -= action;
        }

        public void UnregisterCanceled(Action<T> action)
        {
            Canceled -= action;
        }
    }
}
