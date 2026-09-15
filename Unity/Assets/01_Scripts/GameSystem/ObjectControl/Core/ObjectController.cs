using System.Collections.Generic;
using UnityEngine;

namespace Whatever.ObjectControl
{
    public class ObjectController : MonoBehaviour
    {
        private AgentFactory _factory;
        private Dictionary<int, Agent> _agentByReceiverId;
        private HashSet<Agent> _agents;
        private Agent _player;

        // Test
        private int[] _playerSkill = new int[] { 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4 };

        private void Awake()
        {
            _factory = new();
            _agentByReceiverId = new();
            _agents = new();
            _player = _factory.CreatePlayerAgent();
        }

        private void Update()
        {
            foreach (var item in _agents)
            {
                item.Tick();
            }
        }

        public void ConnectPlayerAgent(IInputReceiver receiver)
        {
            ConnectController(_player, receiver);
            ((PlayerBrain)_player.Brain).SkillSlotSystem.CopySlot(_playerSkill);
            _player.SetActive(true);
        }

        public void DisconnectAgent(IInputReceiver receiver)
        {
            if (_agentByReceiverId.TryGetValue(receiver.ReceiverID, out var agent) == true)
            {
                agent.SetActive(false);
                _agents.Remove(agent);
                _agentByReceiverId.Remove(receiver.ReceiverID);
            }
        }

        private void ConnectController(Agent agent, IInputReceiver receiver)
        {
            if (_agentByReceiverId.ContainsKey(receiver.ReceiverID) == true)
                DisconnectAgent(receiver);

            agent.Connect(receiver);
            _agents.Add(agent);
            _agentByReceiverId.Add(receiver.ReceiverID, agent);
        }

    }
}
