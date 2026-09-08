using Chunity.GameSystem.AI;

namespace Whatever.Entities.Characters
{
    public class CharacterState
    {
        private readonly FSM<MovementState, CharacterStateContext> _movement;

        public MovementState MovementState
        {
            get => _movement.CurrentState;
        }

        public CharacterState(CharacterStateContext context)
        {
            _movement = new(new MovementStateResolver());
            _movement.SetupStates(new IdleState());
            _movement.SetupStates(new RunningState());
            _movement.SetupStates(new SprintingState());
            _movement.Run(context);
        }

        public void Update(CharacterStateContext context)
        {
            _movement.Tick(context);
        }
    }
}
