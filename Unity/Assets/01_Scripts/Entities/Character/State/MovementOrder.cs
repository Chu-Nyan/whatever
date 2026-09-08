using UnityEngine;

namespace Whatever.Entities.Characters
{
    public class MovementOrder
    {
        public enum Order
        {
            Idle, Running, Sprinting
        }

        public Order Requested;
        public Order CurrentOrder;
        public Vector3 Destination;
        public bool IsSprinting;

        public bool IsOrderPlaying;
        public bool IsRequestedOrder;

        public void RequestOrder(Order order)
        {
            Requested = order;
            IsRequestedOrder = true;
        }

        public void ExcuteRequestedOrder()
        {
            CurrentOrder = Requested;
            IsOrderPlaying = true;
            IsRequestedOrder = false;
        }

        public void EndOrder()
        {
            IsOrderPlaying = false;
        }
    }
}
