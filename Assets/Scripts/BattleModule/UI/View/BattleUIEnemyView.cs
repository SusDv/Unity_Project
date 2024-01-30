using UnityEngine;
using UnityEngine.UI;

namespace BattleModule.UI.View
{
    public class BattleUIEnemyView : MonoBehaviour
    {
        [SerializeField] private Image _enemyHealthBar;

        public void SetData(float healthValue) 
        {
            _enemyHealthBar.fillAmount = healthValue;
        }
    }
}
