using BattleModule.ActionCore.Events;
using BattleModule.ActionCore.Interfaces;
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

        private IBattleAction _battleAction;

        public void InitBattleUIAction(
            IBattleAction battleAction) 
        {
            _battleAction = battleAction;

            battleAction.OnBattleActionChanged += UpdateBattleActionInfo;

            _battleActionButton.OnButtonClick += BattleActionPointerClick;

            UpdateBattleActionInfo();
        }

        private void BattleActionPointerClick()
        {
            BattleGlobalEventManager.Instance.InvokeBattleAction();
        }

        private void UpdateBattleActionInfo() 
        {
            _battleActionView.SetData(
                $"<b><u>Action:</u></b> {_battleAction.BattleAction?.ActionName}");
        }
    }
}
