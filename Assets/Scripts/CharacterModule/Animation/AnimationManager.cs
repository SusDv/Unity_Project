using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CharacterModule.Animation
{
    public class AnimationManager : MonoBehaviour
    {
        [SerializeField] private Animator _characterAnimator;
        
        private bool _isAnimationEnded;
        
        public void AnimationEventTriggered()
        {
            _isAnimationEnded = true;
        }

        public async UniTask PlayAnimation(string animationName)
        {
            _isAnimationEnded = false;
            
            _characterAnimator.SetTrigger(animationName);
            
            while (!_isAnimationEnded)
            {
                await UniTask.Yield();
            }
        }
    }
}