using UnityEngine;

namespace Chunity.Core
{
    public interface ISceneObject
    {
        public Transform transform { get; }
        public void SetActive(bool value);
    }
}
