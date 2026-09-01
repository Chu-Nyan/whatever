using System;

namespace Chunity.GameSystem.AI
{
    public interface IMachineStateResolver<T, K> where T : Enum
    {
        public T Resolve(K context);
    }
}
