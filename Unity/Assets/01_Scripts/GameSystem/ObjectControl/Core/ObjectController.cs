using System.Collections.Generic;
using UnityEngine;

namespace Whatever.ObjectControl
{
    public class ObjectController : MonoBehaviour
    {
        private AgentFactory _factory;
        private Dictionary<int, Agent> _agentByReceiverId;
        private HashSet<Agent> _agents;

        private void Awake()
        {
            _factory = new();
            _agentByReceiverId = new();
            _agents = new();
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
            ConnectReceiver(_factory.Player, receiver);
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

        private void ConnectReceiver(Agent agent, IInputReceiver receiver)
        {
            if (_agentByReceiverId.ContainsKey(receiver.ReceiverID) == true)
                DisconnectAgent(receiver);

            _agents.Add(agent);
            _agentByReceiverId.Add(receiver.ReceiverID, agent);
            agent.Connect(receiver);
            agent.SetActive(true);
        }

    }
}
