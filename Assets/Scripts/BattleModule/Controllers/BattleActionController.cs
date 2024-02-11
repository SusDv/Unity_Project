using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions;
using BattleModule.Actions.BattleActions;
using BattleModule.Actions.BattleActions.Base;
using BattleModule.Actions.BattleActions.Context;
using BattleModule.Input;
using BattleModule.Utility.Interfaces;
using VContainer;

namespace BattleModule.Controllers
{
    public class BattleActionController : ICharacterInTurnObserver
    {
        private BattleAction _currentBattleAction;

        private Character _characterToHaveTurn;

        public event Action<BattleActionContext> OnBattleActionChanged;
        
        public event Action OnBattleActionCanceled;
        
        [Inject]
        public BattleActionController(BattleTurnController battleTurnController, BattleInput battleInput) 
        {
            battleTurnController.AddCharacterInTurnObserver(this);
            
            battleInput.OnCancelButtonPressed += OnCancelButtonPressed;
        }
        
        public void SetBattleAction<T>(object actionObject)
            where T : BattleAction
        {
            _currentBattleAction = Activator.CreateInstance<T>();
            
            _currentBattleAction.Init(actionObject);

            OnBattleActionChanged?.Invoke(_currentBattleAction.GetBattleActionContext());
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

        public void Notify(List<Character> charactersToHaveTurn) 
        {
            _characterToHaveTurn = charactersToHaveTurn.First();

            SetDefaultBattleAction();
        }
        
        private void OnCancelButtonPressed()
        {
            if (_currentBattleAction is BattleDefaultAction)
            {
                return;
            }
            
            SetDefaultBattleAction();
            
            OnBattleActionCanceled?.Invoke();
        }
    }
}
