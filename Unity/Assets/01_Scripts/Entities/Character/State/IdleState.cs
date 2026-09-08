using Chunity.GameSystem.AI;

namespace Whatever.Entities.Characters
{
    public class IdleState : IMachineState<MovementOrder.Order, CharacterStateContext>
    {
        public MovementOrder.Order Type
        {
            get => MovementOrder.Order.Idle;
        }

        public void Enter(CharacterStateContext context)
        {
        }

        public bool Tick(CharacterStateContext context)
        {
            if (context.MovementOrder.Requested != MovementOrder.Order.Idle)
                return true;

            return false;
        }

        public void Exit(CharacterStateContext context)
        {

        }
    }
}
