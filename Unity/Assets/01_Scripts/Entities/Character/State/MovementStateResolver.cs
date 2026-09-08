using Chunity.GameSystem.AI;

namespace Whatever.Entities.Characters
{
    public class MovementStateResolver : IMachineStateResolver<MovementState, CharacterStateContext>
    {
        public MovementState Resolve(CharacterStateContext context)
        {
            if (context.MovementOrder.CanMove == false)
                return MovementState.Idle;

            if (context.MovementOrder.IsSprinting == true)
                return MovementState.Sprinting;

            return MovementState.Running;
        }
    }
}
