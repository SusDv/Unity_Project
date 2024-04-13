using System;
using UnityEngine;

namespace CharacterModule.Animation
{
    public class AnimationManager : MonoBehaviour
    {
        [SerializeField] private Animator _characterAnimator;

        private Action _animationEndCallback;
        
        public void AnimationEventTriggered()
        {
            _animationEndCallback?.Invoke();
        }

        public void PlayAnimation(string animationName, Action animationEndCallback)
        {
            _characterAnimator.SetTrigger(animationName);
            
            _animationEndCallback = animationEndCallback;
        }
    }
}