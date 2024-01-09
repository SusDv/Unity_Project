using BattleModule.ActionCore;
using BattleModule.ActionCore.Events;
using BattleModule.UI.Button;
using BattleModule.UI.View;
using UnityEngine;

namespace BattleModule.UI.Presenter
{
    public class BattleUIAction : MonoBehaviour
    {
        [Header("View")]
        [SerializeField] private BattleUIActionView _battleActionView;

        [Header("Battle Button")]
        [SerializeField] private BattleUIButton _battleActionButton;

        public void InitBattleUIAction() 
        {
            _battleActionButton.OnButtonClick += BattleActionPointerClick;
        }

        private void BattleActionPointerClick()
        {
            BattleGlobalActionEvent.InvokeBattleAction();
        }
    }
}
