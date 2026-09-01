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

        public void AddStates(IMachineState<T, K> state)
        {
            if (_stateByType.ContainsKey(state.Type) == true)
                throw new ArgumentException($"중복 등록됨");

            _stateByType[state.Type] = state;
        }

        public void Setup(K context)
        {
            _currentState = _stateByType[_resolver.Resolve(context)];
            _currentState.Enter(context);
        }

        public void Tick(K context)
        {
#if UNITY_EDITOR 
            if (_currentState == null)
                throw new NullReferenceException("현재 상태 초기화가 되지 않음");
#endif
            bool isFinished = _currentState.Tick(context);

            if (_currentState.Tick(context) == true)
                ChangeState(_resolver.Resolve(context), context);
        }

        public void ChangeState(T type, K context)
        {
            if (_currentState.Type.Equals(type) == true)
                return;

            _currentState?.Exit(context);
            _currentState = _stateByType[type];
            //Debug.Log(type);
            _currentState.Enter(context);
        }
    }
}
