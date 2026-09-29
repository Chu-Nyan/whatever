using Chunity.Core;
using Chunity.Utility;
using System;
using System.Collections.Generic;

namespace Chunity.Framework
{
    public class UIManager : Singleton<UIManager>
    {
        private readonly string _pathFormat;
        private readonly Dictionary<Type, UIController> _uiByID;

        public UIManager(string pathFormat) : base()
        {
            _uiByID = new();
            _pathFormat = pathFormat;
        }

        public T GetUI<T>() where T : UIController
        {
            Type type = typeof(T);
            if (_uiByID.TryGetValue(type, out var ui) == false)
            {
                ui = AssetManager.InstantiateAssetSync<T>(string.Format(_pathFormat, type.Name));
                _uiByID[type] = ui;
            }

            return (T)ui;
        }
    }
}
