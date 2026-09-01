using Chunity.Core;
using Unity.Cinemachine;

namespace Chunity.Framework
{
    public interface IVCam : IEntity
    {
        public VCamType Type { get; }

        public CinemachineCamera CinemachineCamera { get; }
    }
}
