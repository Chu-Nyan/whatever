using System;

namespace Chunity.Utility
{
    public class AniClipEvent
    {
        public readonly int ID;
        private AniEventData _data;
        private Action<AniEventData> _action;
        private float _timeing;

        public float Timeing
        {
            get => _timeing;
        }

        public AniClipEvent(int id, AniEventData data, float timeing, Action<AniEventData> action)
        {
            if (timeing > 1 || timeing < 0)
            {
                throw new ArgumentOutOfRangeException($"이벤트 실행 시간이 허용 범위를 이탈함 : {timeing}");
            }
            ID = id;
            _timeing = timeing;
            _data = data;
            _action = action;
        }

        public void Setup(AniEventData data, float timeing, Action<AniEventData> action)
        {
            _data = data;
            _timeing = timeing;
            _action = action;
        }

        public void SetData(AniEventData data)
        {
            _data = data;
        }

        public bool TryExecute(float normalizedTime)
        {
            if (_timeing > normalizedTime)
                return false;

            _action?.Invoke(_data);
            return true;
        }
    }
}
