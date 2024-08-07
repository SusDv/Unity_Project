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

        public static async UniTask SlideOut(RectTransform rectTransform, 
            Vector2 direction, float time, bool visibility)
        {
            var startPosition = rectTransform.anchoredPosition;
            
            var endPosition = startPosition + GetEndPosition(rectTransform, direction) * (visibility ? -1 : 1);
            
            var elapsedTime = 0f;


            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                
                var t = elapsedTime / time;
                
                rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);
                
                await UniTask.Yield();
            }
            
            rectTransform.anchoredPosition = endPosition;
        }

        private static Vector2 GetEndPosition(RectTransform transform, Vector2 direction)
        {
            return direction.y != 0 ? Vector2.up * transform.sizeDelta.y * direction.y : Vector2.right * transform.sizeDelta.x * direction.x;
        }
    }
}