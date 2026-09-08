using Chunity.GameSystem.AI;

namespace Whatever.Entities.Characters
{
    public class IdleState : IMachineState<MovementState, CharacterStateContext>
    {
        public MovementState Type
        {
            get => MovementState.Idle;
        }

        public void Enter(CharacterStateContext context)
        {
        }

        public bool Tick(CharacterStateContext context)
        {
            if (context.MovementOrder.CanMove == true)
                return true;

            return false;
        }

        public void Exit(CharacterStateContext context)
        {

        }
    }
}
