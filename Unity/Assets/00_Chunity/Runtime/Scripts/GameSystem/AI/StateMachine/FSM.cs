using System;
using System.Collections.Generic;

namespace Chunity.GameSystem.AI
{
    public class FSM<T, K> where T : Enum
    {
        private readonly IMachineStateResolver<T, K> _resolver;
        private readonly Dictionary<T, IMachineState<T, K>> _stateByType;
        private IMachineState<T, K> _currentState;

        public T CurrentState
        {
            get => _currentState.Type;
        }

        public FSM(IMachineStateResolver<T, K> resolver)
        {
            _stateByType = new();
            _resolver = resolver;
        }

        public void SetupStates(IMachineState<T, K> state)
        {
            if (_stateByType.ContainsKey(state.Type) == true)
                throw new ArgumentException($"중복 등록됨");

            _stateByType[state.Type] = state;
        }

        public void Run(K context)
        {
            ChangeState(_resolver.Resolve(context), context);
        }

        public void Tick(K context)
        {
#if UNITY_EDITOR 
            if (_currentState == null)
                throw new NullReferenceException("현재 상태 초기화가 되지 않음");
#endif
            bool isFinished = _currentState.Tick(context);

            if (isFinished == true)
            {
                _currentState.Exit(context);
                var nextState = _resolver.Resolve(context);
                if (_currentState.Type.Equals(nextState) == false)
                {
                    ChangeState(_resolver.Resolve(context), context);
                }
            }
        }

        private void ChangeState(T type, K context)
        {
            _currentState = _stateByType[type];
            _currentState.Enter(context);
        }
    }
}
