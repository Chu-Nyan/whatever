using Chunity.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chunity.Utility
{
    public class MeshSlotHub : MonoBehaviour
    {
        [SerializeField]
        private SerializablePair<string, Component>[] _meshComponents;
        private Dictionary<int, IMeshAdapter> _adapterByPart;

        private void Awake()
        {
            _adapterByPart = new Dictionary<int, IMeshAdapter>();
            for (int i = 0; i < _meshComponents.Length; i++)
            {
                SerializablePair<string, Component> item = _meshComponents[i];
                IMeshAdapter handler = ConvertMeshHandler(item.Value);

#if (UNITY_EDITOR)
                if (_adapterByPart.TryAdd(item.GetHashCode(), handler) == false)
                    Debug.Log("중복 이름 추가");
#endif
                _adapterByPart[item.Key.GetHashCode()] = handler;
            }
        }

#if (UNITY_EDITOR)
        private void OnValidate()
        {
            for (int i = 0; i < _meshComponents.Length; i++)
            {
                Component component = _meshComponents[i].Value;

                if (component is MeshFilter || component is SkinnedMeshRenderer || component == null)
                    continue;

                if (component.TryGetComponent<MeshFilter>(out var filter) == true)
                {
                    _meshComponents[i].Value = filter;
                }
                else if (component.TryGetComponent<SkinnedMeshRenderer>(out var skinned) == true)
                {
                    _meshComponents[i].Value = skinned;
                }
                else
                {
                    _meshComponents[i].Value = null;
                    Debug.Log("메쉬를 지원하지 않는 게임 오브젝트가 추가됨");
                }
            }
        }
#endif

        public void SetMesh(int hash, Mesh mesh)
        {
#if (UNITY_EDITOR)
            if (_adapterByPart == null)
                Awake();
#endif
            if (_adapterByPart.ContainsKey(hash) == false)
            {
                Debug.Log("Slot이 존재하지 않음");
                return;
            }
            _adapterByPart[hash].SetMesh(mesh);
        }

        public void SetMesh(string part, Mesh mesh)
        {
            SetMesh(part.GetHashCode(), mesh);
        }

        private IMeshAdapter ConvertMeshHandler(Component renderer)
        {
            if (renderer is SkinnedMeshRenderer skinned)
                return new SkinnedMeshAdapter(skinned);
            else if (renderer is MeshFilter filter)
                return new MeshFilterAdapter(filter);
            else
                throw new InvalidCastException("메쉬 기능을 지원하지 않는 컴포넌트가 추가됨");
        }
    }
}
