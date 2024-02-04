using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions;
using BattleModule.Actions.BattleActions;
using BattleModule.Actions.BattleActions.Base;
using BattleModule.Actions.BattleActions.Context;
using BattleModule.Input;

namespace BattleModule.Controllers
{
    public class BattleActionController
    {
        private BattleAction _currentBattleAction;

        private Character _characterToHaveTurn;

        public event Action<BattleActionContext> OnBattleActionChanged;
        
        public event Action OnBattleActionCanceled;

        public BattleActionController(BattleTurnController battleTurnController, BattleInput battleInput) 
        {
            battleTurnController.OnCharacterToHaveTurnChanged += OnCharacterToHaveTurnChanged;
            
            battleInput.OnCancelButtonPressed += OnCancelButtonPressed;
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
        
        private void OnCancelButtonPressed()
        {
            SetDefaultBattleAction();
            
            OnBattleActionCanceled?.Invoke();
        }

    }
}
