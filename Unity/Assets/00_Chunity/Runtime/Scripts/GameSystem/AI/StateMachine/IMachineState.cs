using System;

namespace Chunity.GameSystem.AI
{
    public interface IMachineState<T, K> where T : Enum
    {
        public T Type { get; }
        void Enter(K context);
        bool Tick(K context);
        void Exit(K context);
    }
}
