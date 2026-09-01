using System;
using UnityEngine.InputSystem;

namespace Chunity.Framework
{
    public class FixedInputEvent<T>
    {
        private readonly T _value;
        private event Action<T> Performed;
        private event Action<T> Canceled;

        public FixedInputEvent(InputAction action, InputEventType type, T value)
        {
            _value = value;

            if ((type & InputEventType.Performed) != 0)
                action.performed += InvokePerformed;

            if ((type & InputEventType.Canceled) != 0)
                action.canceled += InvokeCanceled;
        }

        private void InvokePerformed(InputAction.CallbackContext value)
        {
            Performed?.Invoke(_value);
        }

        private void InvokeCanceled(InputAction.CallbackContext value)
        {
            Canceled?.Invoke(_value);
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
