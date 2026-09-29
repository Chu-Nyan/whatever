using System;
using UnityEngine;

namespace Whatever.GameSystems.Stat
{
    public class Resource
    {
        private float _value;
        public event Action<float> Changed;

        public float Value => _value;

        public void Add(float value, float maxValue)
        {
            Set(Mathf.Clamp(_value + value, 0, maxValue));
        }

        public void Set(float value)
        {
            _value = value;
            Changed?.Invoke(_value);
        }
    }
}
