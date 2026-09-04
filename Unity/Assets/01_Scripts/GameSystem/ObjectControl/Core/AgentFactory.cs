namespace Whatever.ObjectControl
{
    public class AgentFactory
    {
        private readonly Agent _player;

        public Agent Player
        {
            get => _player;
        }

        public AgentFactory()
        {
            _player = new Agent(new PlayerBrain());
        }
    }
}
