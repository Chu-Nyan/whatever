using UnityEngine;

namespace Whatever.ObjectControl
{
    public interface IMovementReceiver : IInputReceiver
    {
        public Transform transform { get; }
        public void SetDestination(Vector3 dir);
        public void ToggleSprint(bool value);
        public void OrderDodge(Vector2 dir);
    }
}
