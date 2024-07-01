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

        public async UniTask PlayAnimation(string animationName, 
            float triggerPercentage = 0.5f)
        {
            try
            {
                _cancellationToken.ThrowIfCancellationRequested();
                
                _characterAnimator.SetTrigger(animationName);

                var animatorStateInfo = _characterAnimator.GetCurrentAnimatorStateInfo(0);

                while (_characterAnimator != null && !animatorStateInfo.IsName(animationName) ||
                       animatorStateInfo.normalizedTime < triggerPercentage)
                {
                    await UniTask.Yield();
                
                    _cancellationToken.ThrowIfCancellationRequested();
                
                    animatorStateInfo = _characterAnimator.GetCurrentAnimatorStateInfo(0);
                }
            }
            catch (OperationCanceledException) { }
        }

        private void OnApplicationQuit()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}