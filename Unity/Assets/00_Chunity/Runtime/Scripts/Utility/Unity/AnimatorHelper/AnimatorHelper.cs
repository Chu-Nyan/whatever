using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chunity.Utility
{
    /// <summary>
    /// 애니메이터 해쉬 접근을 도와줌
    /// </summary>
    /// <typeparam name="T">파라미터 대응</typeparam>
    /// <typeparam name="K">스테이트 대응</typeparam>
    public class AnimatorHelper<T, K> where T : Enum where K : Enum
    {
        private readonly Animator _animator;
        private readonly Dictionary<T, int> _idByEnum;
        private readonly Dictionary<int, K> _hashByState;
        private readonly Dictionary<K, AniStateBehaviour> _eventByState;
        private K _state;

        public RuntimeAnimatorController RuntimeAnaimator
        {
            get => _animator.runtimeAnimatorController;
        }

        public AnimatorHelper(Animator animator, Dictionary<T, string> clipStrings, Dictionary<K, string> stateStrings)
        {
            _animator = animator;
            _idByEnum = new(clipStrings.Count);
            _hashByState = new(stateStrings.Count);
            _eventByState = new(stateStrings.Count);

            foreach (var item in clipStrings)
            {
                int hash = Animator.StringToHash(item.Value);
                _idByEnum[item.Key] = hash;
            }

            foreach (var item in stateStrings)
            {
                int hash = Animator.StringToHash(item.Value);
                _hashByState[hash] = item.Key;
                _eventByState[item.Key] = new AniStateBehaviour();
            }
        }

        public void Tick()
        {
            AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);
            K nextState = _hashByState[info.shortNameHash];
            if (_state.Equals(nextState) == false)
            {
                _eventByState[_state].OnStateExit(info);
                _eventByState[nextState].OnStateEnter(info);
                _state = nextState;
            }

            _eventByState[_state].OnStateUpdate(info);
        }

        public AnimatorStateInfo GetCurrentStateInfo()
        {
            return _animator.GetCurrentAnimatorStateInfo(0);
        }

        public K GetStateHash(int key)
        {
            return _hashByState[key];
        }

        public void RegisterStateEvent(K state, string id, AniEventData data, float timeing, Action<AniEventData> action)
        {
            _eventByState[state].RegisterEvent(id, data, timeing, action);
        }

        public void ChagneEventData(K state, string id, AniEventData data)
        {
            _eventByState[state].ChangeAniEventData(id, data);
        }

        public void SetRuntimeAnimator(RuntimeAnimatorController controller)
        {
            _animator.runtimeAnimatorController = controller;
        }

        public void SetBool(T key, bool value)
        {
#if UNITY_EDITOR == false
            if (_idByEnum.ContainsKey(key) == false)
                return;
#endif

            _animator.SetBool(_idByEnum[key], value);
        }

        public void SetInt(T key, int value)
        {
#if UNITY_EDITOR == false
            if (_idByEnum.ContainsKey(key) == false)
                return;
#endif

            _animator.SetInteger(_idByEnum[key], value);
        }

        public void SetFloat(T key, float value)
        {
#if UNITY_EDITOR == false
            if (_idByEnum.ContainsKey(key) == false)
                return;
#endif  

            _animator.SetFloat(_idByEnum[key], value);
        }

        public void SetTrigger(T key)
        {
            _animator.SetTrigger(_idByEnum[key]);
        }
    }
}
