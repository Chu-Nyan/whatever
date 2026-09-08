using Chunity.GameSystem.AI;

namespace Whatever.Entities.Characters
{
    public class SprintingState : IMachineState<MovementOrder.Order, CharacterStateContext>
    {
        public MovementOrder.Order Type
        {
            get => MovementOrder.Order.Sprinting;
        }

        public void Enter(CharacterStateContext context)
        {
        }

        public bool Tick(CharacterStateContext context)
        {
            if (context.MovementOrder.Requested != MovementOrder.Order.Sprinting)
                return true;

            return false;
        }

        public void Exit(CharacterStateContext context)
        {
        }
    }
}
