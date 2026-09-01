using Chunity.Core;
using Chunity.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace Chunity.Framework
{
    public class CameraFactory : Singleton<CameraFactory>
    {
        private const string _staticPath = "VCamStatic";
        private const string _followPath = "VCamFollow";

        private Transform _root;
        private Dictionary<VCamType, ObjectPooling<IVCam>> _pooling;

        public CameraFactory()
        {
            _root = new GameObject("Camera").transform;
            _pooling = new();
            _pooling.Add(VCamType.Static, new(() => AssetManager.InstantiateAssetSync<VCamStatic>(_staticPath, "StaticCamera", _root)));
            _pooling.Add(VCamType.Follow, new(() => AssetManager.InstantiateAssetSync<VCamFollow>(_followPath, "FollowCamera", _root)));
        }

        public VCamFollow CreateFollow(VCamFollowData data)
        {
            var camera = (VCamFollow)_pooling[VCamType.Follow].Dequeue();
            camera.Setup(data);
            camera.ResgierDeactivated(Enqueue);

            return camera;
        }

        public VCamStatic CreateStatic(VCamStaticData data)
        {
            var camera = (VCamStatic)_pooling[VCamType.Static].Dequeue();
            camera.Setup(data);
            camera.ResgierDeactivated(Enqueue);

            return camera;
        }

        public void Enqueue(IVCam cam)
        {
            _pooling[cam.Type].Enqueue(cam);
        }
    }
}
