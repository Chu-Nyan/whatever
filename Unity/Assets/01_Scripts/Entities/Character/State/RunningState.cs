using Chunity.GameSystem.AI;

namespace Whatever.Entities.Characters
{
    public class RunningState : IMachineState<MovementState, CharacterStateContext>
    {
        public MovementState Type
        {
            get => MovementState.Running;
        }

        public void Enter(CharacterStateContext context)
        {
        }

        public bool Tick(CharacterStateContext context)
        {
            if (context.MovementOrder.CanMove == false)
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
