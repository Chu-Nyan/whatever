using Chunity.GameSystem.AI;

namespace Whatever.Entities.Characters
{
    public class RunningState : IMachineState<MovementOrder.Order, CharacterStateContext>
    {
        public MovementOrder.Order Type
        {
            get => MovementOrder.Order.Running;
        }

        public void Enter(CharacterStateContext context)
        {
        }

        public bool Tick(CharacterStateContext context)
        {
            if (context.MovementOrder.Requested != MovementOrder.Order.Running)
                return true;
            if (context.MovementOrder.IsSprinting == true)
                return true;

            return false;
        }

        public void Exit(CharacterStateContext context)
        {
        }
    }
}
