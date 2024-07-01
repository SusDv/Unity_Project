using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Utility.Animation
{
    public static class AnimationService
    {
        public static async UniTask AnimateFromValue(float start, float end, float time)
        {
            float step = Mathf.Abs(start - end) / time;

            float result = start;

            while (!Mathf.Approximately(result, end))
            {
                result += step;

                await UniTask.Yield();
            }
        }
    }
}