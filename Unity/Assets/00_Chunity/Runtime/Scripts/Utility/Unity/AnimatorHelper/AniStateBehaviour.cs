using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chunity.Utility
{
    /// <summary>
    /// 중복 아이디는 덮어쓰기됨
    /// </summary>
    public class AniStateBehaviour
    {
        private readonly Dictionary<int, AniClipEvent> _clipEventById;
        private readonly HashSet<AniClipEvent> _enterEvents;
        private readonly List<AniClipEvent> _progressEvents;
        private readonly HashSet<AniClipEvent> _exitEvents;
        private int _playIndex;
        private float _beforeNormalizedTime;

        public AniStateBehaviour()
        {
            _enterEvents = new();
            _progressEvents = new();
            _exitEvents = new();
            _clipEventById = new();
        }

        public void RegisterEvent(string id, AniEventData data, float timeing, Action<AniEventData> action)
        {
            int hashID = id.GetHashCode();
            if (_clipEventById.TryGetValue(hashID, out AniClipEvent clipEvent) == true)
                RemoveEvent(clipEvent.ID);
            else
            {
                clipEvent = new(hashID, data, timeing, action);
                _clipEventById[hashID] = clipEvent;
            }

            if (clipEvent.Timeing <= 0)
                _enterEvents.Add(clipEvent);
            else if (clipEvent.Timeing >= 1)
                _exitEvents.Add(clipEvent);
            else
            {
                _progressEvents.Add(clipEvent);
                _progressEvents.Sort((a, b) => a.Timeing.CompareTo(b.Timeing));
            }
        }

        public void ChangeAniEventData(string id, AniEventData data)
        {
            if (_clipEventById.TryGetValue(id.GetHashCode(), out AniClipEvent clipEvent) == false)
                throw new KeyNotFoundException($"{id} 누락");

            clipEvent.SetData(data);
        }

        public void OnStateEnter(AnimatorStateInfo stateInfo)
        {
            ResetEvent();
            foreach (var item in _enterEvents)
            {
                item.TryExecute(0);
            }
        }

        public void OnStateUpdate(AnimatorStateInfo stateInfo)
        {
            if (_playIndex >= _progressEvents.Count)
                return;
            if (_beforeNormalizedTime > stateInfo.normalizedTime)
                ResetEvent();

            bool isSucceed = true;
            while (isSucceed == true && _playIndex < _progressEvents.Count)
            {
                isSucceed = _progressEvents[_playIndex].TryExecute(stateInfo.normalizedTime);
                if (isSucceed == true)
                    _playIndex++;
            }

            _beforeNormalizedTime = stateInfo.normalizedTime;
        }

        public void OnStateExit(AnimatorStateInfo stateInfo)
        {
            foreach (var item in _exitEvents)
            {
                item.TryExecute(1);
            }
        }

        public void RemoveEvent(int id)
        {
            if (_clipEventById.TryGetValue(id, out AniClipEvent clipEvent) == false)
                return;

            if (clipEvent.Timeing <= 0)
                _enterEvents.Remove(clipEvent);
            else if (clipEvent.Timeing >= 1)
                _exitEvents.Remove(clipEvent);
            else
                _progressEvents.Remove(clipEvent);
        }

        public void RemoveEvent(string id)
        {
            RemoveEvent(id.GetHashCode());
        }

        private void ResetEvent()
        {
            _playIndex = 0;
        }
    }
}
