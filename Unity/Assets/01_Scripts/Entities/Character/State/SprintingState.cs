using Chunity.GameSystem.AI;

namespace Whatever.Entities.Characters
{
    public class SprintingState : IMachineState<MovementState, CharacterStateContext>
    {
        public MovementState Type
        {
            get => MovementState.Sprinting;
        }

        public void Enter(CharacterStateContext context)
        {
        }

        public bool Tick(CharacterStateContext context)
        {
            if (context.MovementOrder.CanMove == false)
                return true;
            if (context.MovementOrder.IsSprinting == false)
                return true;

            return false;
        }

        public void Exit(CharacterStateContext context)
        {
        }
    }
}
