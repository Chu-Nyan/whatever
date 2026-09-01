using UnityEngine;

namespace Chunity.Core
{
    public interface IEntity
    {
        public Transform transform { get; }
        public void SetActive(bool value);
    }
}
