using UnityEngine;

namespace Whatever.ObjectControl
{
    public interface IMovementReceiver : IInputReceiver
    {
        public Transform transform { get; }
        public void SetDestination(Vector3 dir);
    }
}
