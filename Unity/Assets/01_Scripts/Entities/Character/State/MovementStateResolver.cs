using Chunity.GameSystem.AI;

namespace Whatever.Entities.Characters
{
    public class MovementStateResolver : IMachineStateResolver<MovementOrder.Order, CharacterStateContext>
    {
        public MovementOrder.Order Resolve(CharacterStateContext context)
        {
            if (context.MovementOrder.IsRequestedOrder == false)
                return MovementOrder.Order.Idle;
            if (context.ActionOrder.CanMove == false)
                return MovementOrder.Order.Idle;

            if (context.MovementOrder.Requested == MovementOrder.Order.Running)
            {
                if (context.MovementOrder.IsSprinting == true)
                    return MovementOrder.Order.Sprinting;

                return MovementOrder.Order.Running;
            }

            return MovementOrder.Order.Idle;
        }
    }

    public class ActionStateResolver : IMachineStateResolver<ActionOrder.Order, CharacterStateContext>
    {
        public ActionOrder.Order Resolve(CharacterStateContext context)
        {
            if (context.ActionOrder.IsRequestedOrder == false)
                return ActionOrder.Order.Idle;
            if (context.ActionOrder.Requested == ActionOrder.Order.Dodge)
            {
                return ActionOrder.Order.Dodge;
            }

            return ActionOrder.Order.Idle;
        }
    }
}
