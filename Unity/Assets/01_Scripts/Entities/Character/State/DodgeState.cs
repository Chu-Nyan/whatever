using Chunity.GameSystem.AI;

namespace Whatever.Entities.Characters
{
    public class DodgeState : IMachineState<ActionOrder.Order, CharacterStateContext>
    {
        public ActionOrder.Order Type
        {
            get => ActionOrder.Order.Dodge;
        }

        public void Enter(CharacterStateContext context)
        {
        }

        public bool Tick(CharacterStateContext context)
        {
            if (context.ActionOrder.IsOrderPlaying == true)
                return false;

            return true;
        }

        public void Exit(CharacterStateContext context)
        {
        }
    }

    public class ActionIdleState : IMachineState<ActionOrder.Order, CharacterStateContext>
    {
        public ActionOrder.Order Type
        {
            get => ActionOrder.Order.Idle;
        }

        public void Enter(CharacterStateContext context)
        {
        }

        public bool Tick(CharacterStateContext context)
        {
            if (context.ActionOrder.IsRequestedOrder == true)
                return true;

            return false;
        }

        public void Exit(CharacterStateContext context)
        {
        }
    }
}
