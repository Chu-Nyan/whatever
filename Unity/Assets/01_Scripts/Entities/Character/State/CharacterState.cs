using Chunity.GameSystem.AI;

namespace Whatever.Entities.Characters
{
    public class CharacterState
    {
        private readonly FSM<MovementOrder.Order, CharacterStateContext> _movement;
        private readonly FSM<ActionOrder.Order, CharacterStateContext> _action;

        public MovementOrder.Order MovementState
        {
            get => _movement.CurrentState;
        }

        public ActionOrder.Order ActionState
        {
            get => _action.CurrentState;
        }

        public CharacterState(CharacterStateContext context)
        {
            _movement = new(new MovementStateResolver());
            _movement.SetupStates(new IdleState());
            _movement.SetupStates(new RunningState());
            _movement.SetupStates(new SprintingState());
            _movement.Run(context);

            _action = new(new ActionStateResolver());
            _action.SetupStates(new ActionIdleState());
            _action.SetupStates(new DodgeState());
            _action.Run(context);
        }

        public void Update(CharacterStateContext context)
        {
            _movement.Tick(context);
            _action.Tick(context);
        }
    }
}
