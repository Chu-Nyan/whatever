using Chunity.Data;
using Chunity.DataPipeline;
using Chunity.Framework;
using Chunity.GameSystem.Physics;
using Chunity.Utility;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace Chunity.Core
{
    public class ChuEngine : Singleton<ChuEngine>
    {
        private readonly GameObject _root;
        private readonly TextTable<string> _textResource;
        private readonly NyanCollisonSystem _collisionSys;

        public TextTable<string> TextResource
        {
            get => _textResource;
        }

        public ChuEngine(EngineSetting setting, Transform parent) : base()
        {
            if (parent == null)
                _root = new GameObject("ChuEngine");
            else
                _root = parent.gameObject;

            _textResource = new();
            new UIManager(setting.UIFilePathFormat);
            var json = ExternalFolderHandler.GetLanguagesTextFile(setting.Language);
            _textResource.LoadTexts(JsonConvert.DeserializeObject<Dictionary<string, string>>(json));

            if (setting.UseCollision == true)
                _collisionSys = RunCollisionSystem(setting.CollisionBound, setting.Capacity);
        }


        public static void Run(EngineSetting setting, Transform parent = null)
        {
            new ChuEngine(setting, parent);
        }

        private NyanCollisonSystem RunCollisionSystem(RectBound bound, int capacity)
        {
            var collisionSys = _root.AddComponent<NyanCollisonSystem>();
            collisionSys.InitArray(capacity);
            collisionSys.InitBound(bound);

            var nyanColliderGenerator = new NyanColliderFactory();
            nyanColliderGenerator.Init(collisionSys);
            return collisionSys;
        }
    }
}
