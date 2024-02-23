using System;
using BattleModule.Actions.BattleActions.Context;
using BattleModule.Controllers;
using BattleModule.UI.Presenter.SceneSettings.Action;
using UnityEngine;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUIAction : MonoBehaviour
    {
        private BattleActionSceneSettings _battleActionSceneSettings;
        
        [Inject]
        private void Init(BattleActionSceneSettings battleActionSceneSettings, 
            BattleActionController battleActionController)
        {
            _battleActionSceneSettings = battleActionSceneSettings;
            
            battleActionController.OnBattleActionChanged += OnBattleActionChanged;
        }

        private void OnBattleActionChanged(BattleActionContext context) 
        {
            _battleActionSceneSettings.BattleActionView.SetData(
                $"<b><u>Action:</b></u> {context.ActionName}");
        }
    }
}
