using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Chunity.Framework
{
    public class VCamStatic : MonoBehaviour, IVCam
    {
        [SerializeField]
        private CinemachineCamera _vcam;

        private event Action<VCamStatic> Deactivated;

        public VCamType Type
        {
            get => VCamType.Static;
        }

        public CinemachineCamera CinemachineCamera
        {
            get => _vcam;
        }

        public void Setup(VCamStaticData data)
        {
            _vcam.transform.SetPositionAndRotation(data.Position, data.Rotation);
            _vcam.Lens.FieldOfView = data.POV;
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
            Deactivated?.Invoke(this);
            Deactivated = null;
        }

        public void ResgierDeactivated(Action<VCamStatic> callback)
        {
            Deactivated += callback;
        }

        public void UnresgierDeactivated(Action<VCamStatic> callback)
        {
            Deactivated -= callback;
        }
    }
}
