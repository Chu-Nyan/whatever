using Chunity.Core;
using UnityEngine;

namespace Whatever.Core
{
    public class TestSceneTrigger : MonoBehaviour
    {
        [SerializeField]
        private EngineSetting _engineSetting;

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
