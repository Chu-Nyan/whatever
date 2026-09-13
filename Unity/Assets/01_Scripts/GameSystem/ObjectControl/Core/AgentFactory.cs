namespace Whatever.ObjectControl
{
    public class AgentFactory
    {
        private Agent _player;

        public AgentFactory()
        {
        }

        public Agent CreatePlayerAgent()
        {
            _player = new Agent(new PlayerBrain());
            return _player;
        }
    }
}
