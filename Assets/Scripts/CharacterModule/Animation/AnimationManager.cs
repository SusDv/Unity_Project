using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CharacterModule.Animation
{
    public class AnimationManager : MonoBehaviour
    {
        [SerializeField] private Animator _characterAnimator;
        
        public async UniTask PlayAnimation(string animationName, float triggerPercentage = 0.5f)
        {
            _characterAnimator.SetTrigger(animationName);

            var animatorStateInfo = _characterAnimator.GetCurrentAnimatorStateInfo(0);
            
            while (!animatorStateInfo.IsName(animationName) || animatorStateInfo.normalizedTime < triggerPercentage)
            {
                await UniTask.Yield();
                
                animatorStateInfo = _characterAnimator.GetCurrentAnimatorStateInfo(0);
            }
        }
    }
}