namespace Whatever.Entities.Characters
{
    public class CharacterStateContext
    {
        public readonly MovementOrder MovementOrder;
        public readonly ActionOrder ActionOrder;

        public CharacterStateContext()
        {
            MovementOrder = new();
            ActionOrder = new();
        }
    }
}
