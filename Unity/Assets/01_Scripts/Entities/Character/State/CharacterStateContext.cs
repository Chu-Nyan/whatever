namespace Whatever.Entities.Characters
{
    public class CharacterStateContext
    {
        public MovementOrder MovementOrder;
        public bool ExcuteMovement;

        public CharacterStateContext()
        {
            MovementOrder = new();
        }
    }
}
