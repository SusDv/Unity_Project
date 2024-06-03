using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BattleModule.UI.View
{
    public class BattleUITargetView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _targetCountText;
        
        private readonly List<RectTransform> _targetImages = new ();

        public void AddTarget(RectTransform target)
        {
            target.transform.SetParent(transform);

            target.anchoredPosition3D = Vector3.zero;

            target.gameObject.SetActive(_targetImages.Count < 1);
            
            _targetImages.Add(target);

            _targetCountText.text = _targetImages.Count > 1 ? _targetImages.Count.ToString() : string.Empty;
        }

        public void ClearTargets(Transform baseParent)
        {
            if (_targetImages.Count == 0)
            {
                return;
            }

            foreach (var target in _targetImages)
            {
                target.SetParent(baseParent);
                
                target.gameObject.SetActive(false);
            }

            _targetCountText.text = string.Empty;
            
            _targetImages.Clear();
        }
    }
}