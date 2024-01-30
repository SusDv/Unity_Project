using BattleModule.Actions;
using BattleModule.Actions.BattleActions.Context;
using BattleModule.Controllers;
using BattleModule.UI.Button;
using BattleModule.UI.View;
using UnityEngine;

namespace BattleModule.UI.Presenter
{
    public class BattleUIAction : MonoBehaviour
    {
        [Header("View")]
        [SerializeField] private BattleUIActionView _battleActionView;

        [Header("BattleModule Button")]
        [SerializeField] private BattleUIDefaultButton _battleActionButton;

        private BattleActionController _battleActionController;

        public void Init(
            BattleActionController battleActionController) 
        {
            _battleActionController = battleActionController;

            _battleActionController.OnBattleActionChanged += OnBattleActionChanged;

            _battleActionButton.OnButtonClick += BattleActionPointerClick;
        }

        private void BattleActionPointerClick(object o)
        {
            BattleEventManager.Instance.OnActionButtonPressed?.Invoke();
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
