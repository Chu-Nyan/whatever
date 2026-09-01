using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chunity.Data
{
    [Serializable]
    public class VariableParams
    {
        [SerializeField]
        private Dictionary<string, VariableParam> _params;

        public VariableParam this[string key]
        {
            get => _params[key];
            set => _params[key] = value;
        }

        public VariableParams()
        {
            _params = new();
        }

        public void Add(string key, ParameterType type)
        {
            _params.Add(key, new(type));
        }

        public bool TryGetValue(string key, out VariableParam param)
        {
            return _params.TryGetValue(key, out param);
        }

        public void Clear()
        {
            _params.Clear();
        }

    }
}

