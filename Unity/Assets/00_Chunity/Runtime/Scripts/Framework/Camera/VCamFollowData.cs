using UnityEngine;

namespace Chunity.Framework
{
    public class VCamFollowData
    {
        // VCam
        public Quaternion Rotation;
        public float FieldOfView;

        // Follow
        public Vector3 FollowOffset;
        public Vector3 PositionDamping;
        public Transform TargetObj;
        public Transform LookAtObj;

        public VCamFollowData(Quaternion rotation, float fov, Vector3 followOffset, Vector3 damping, Transform target, Transform lookAt)
        {
            Rotation = rotation;
            FieldOfView = fov;
            FollowOffset = followOffset;
            PositionDamping = damping;
            TargetObj = target;
            LookAtObj = lookAt;
        }
    }
}
