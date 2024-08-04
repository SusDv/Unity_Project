using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CharacterModule.Animation
{
    public class AnimationManager : MonoBehaviour
    {
        [SerializeField] private Animator _characterAnimator;

        private CancellationTokenSource _cancellationTokenSource;

        private CancellationToken _cancellationToken;

        private void Awake()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            _cancellationToken = _cancellationTokenSource.Token;
        }

        public async UniTask<bool> PlayAnimation(string animationName,
            Action triggerCallback = null, float triggerPercentage = 0.5f)
        {
            try
            {
                _cancellationToken.ThrowIfCancellationRequested();

                _characterAnimator.SetTrigger(animationName);

                var animatorStateInfo = _characterAnimator.GetCurrentAnimatorStateInfo(0);

                // Wait until the animation reaches the trigger percentage
                while (_characterAnimator != null && (!animatorStateInfo.IsName(animationName) ||
                                                      animatorStateInfo.normalizedTime < triggerPercentage))
                {
                    await UniTask.Yield();

                    _cancellationToken.ThrowIfCancellationRequested();

                    animatorStateInfo = _characterAnimator.GetCurrentAnimatorStateInfo(0);
                }

                // Invoke the callback
                triggerCallback?.Invoke();

                // Wait until the animation finishes
                while (_characterAnimator != null && animatorStateInfo.IsName(animationName) &&
                       animatorStateInfo.normalizedTime < 1.0f)
                {
                    await UniTask.Yield();

                    _cancellationToken.ThrowIfCancellationRequested();

                    animatorStateInfo = _characterAnimator.GetCurrentAnimatorStateInfo(0);
                }
            }
            catch (OperationCanceledException)
            {
                return false;
            }

            return true;
        }

        private void OnApplicationQuit()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}