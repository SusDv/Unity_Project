using BattleModule.ActionCore.Context;
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
        [SerializeField] private BattleUIDefaultButton _battleActionButton;

        private IBattleAction _battleActionController;

        public void InitBattleUIAction(
            IBattleAction battleActionController) 
        {
            _battleActionController = battleActionController;

            _battleActionController.OnBattleActionChanged += OnBattleActionChanged;

            _battleActionButton.OnButtonClick += BattleActionPointerClick;
        }

        private void BattleActionPointerClick(object o)
        {
            BattleGlobalEventManager.Instance.InvokeBattleAction();
        }

        private void OnBattleActionChanged(BattleActionContext context) 
        {
            _battleActionView.SetData(
                $"<b><u>Action:</b></u> {context.ActionName}");
        }

        private void OnApplicationQuit()
        {
            _battleActionController.OnBattleActionChanged -= OnBattleActionChanged;
        }
    }
}
