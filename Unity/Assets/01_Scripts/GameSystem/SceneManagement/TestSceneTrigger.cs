using Chunity.Core;
using Unity.VisualScripting;
using UnityEngine;
using Whatever.Entities.Characters;
using Whatever.ObjectControl;

namespace Whatever.Core
{
    public class TestSceneTrigger : MonoBehaviour
    {
        [SerializeField]
        private EngineSetting _engineSetting;
        private ObjectController _controller;
        [SerializeField]
        private Character _character;
        private void Awake()
        {
            ChuEngine.Run(_engineSetting, transform.parent);
            CreateStatic();
            CreateInstance();

            InitStatic();
            InitInstance();

            CreateFacade();
            InitFacade();

            GameStart();
        }

        private void CreateStatic()
        {
        }

        private void CreateInstance()
        {
            _controller = transform.AddComponent<ObjectController>();
            _controller.ConnectPlayerAgent(_character);
        }

        private void InitStatic()
        {
        }

        private void InitInstance()
        {
        }

        private void CreateFacade()
        {
        }

        private void InitFacade()
        {
        }

        private void GameStart()
        {
        }
    }
}
