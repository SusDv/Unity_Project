using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions;
using BattleModule.Actions.BattleActions;
using BattleModule.Actions.BattleActions.Base;
using BattleModule.Actions.BattleActions.Context;

namespace BattleModule.Controllers
{
    public class BattleActionController
    {
        private BattleAction _currentBattleAction;

        private Character _characterToHaveTurn;

        public event Action<BattleActionContext> OnBattleActionChanged;

        public BattleActionController(BattleTurnController battleTurnController) 
        {
            battleTurnController.OnCharacterToHaveTurnChanged += OnCharacterToHaveTurnChanged;
        }

        public void SetBattleAction<T>(object actionObject)
            where T : BattleAction
        {
            _currentBattleAction = Activator.CreateInstance<T>().GetInstance(actionObject);

            OnBattleActionChanged?.Invoke(_currentBattleAction.GetBattleActionContext());
        }

        public void ResetBattleAction() 
        {
            SetDefaultBattleAction();
        }

        public void ExecuteBattleAction(List<Character> targets) 
        {
            _currentBattleAction.PerformAction(_characterToHaveTurn.GetCharacterStats(), targets);

            BattleEventManager.Instance.AdvanceTurn();
        }

        private void SetDefaultBattleAction() 
        {
            SetBattleAction<BattleDefaultAction>(_characterToHaveTurn.
                GetCharacterWeapon().GetWeapon());
        }

        private void OnCharacterToHaveTurnChanged(List<Character> charactersToHaveTurn) 
        {
            _characterToHaveTurn = charactersToHaveTurn.First();

            SetDefaultBattleAction();
        }
    }
}
