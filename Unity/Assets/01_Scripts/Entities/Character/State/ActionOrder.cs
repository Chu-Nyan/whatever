using UnityEngine;

namespace Whatever.Entities.Characters
{
    public class ActionOrder
    {
        public enum Order
        {
            Idle, Dodge
        }

        public Order Requested;
        public Order CurrentOrder;
        public Vector2 DodgeDirection;
        public bool CanMove = true;

        public float OrderTime;
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
            OrderTime = 0;
            IsOrderPlaying = false;
            CanMove = true;
        }
    }
}
