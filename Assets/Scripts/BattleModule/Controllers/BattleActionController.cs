using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions;
using BattleModule.Actions.BattleActions;
using BattleModule.Actions.BattleActions.Base;
using BattleModule.Actions.BattleActions.Context;
using BattleModule.Controllers.Turn;
using BattleModule.Input;
using CharacterModule;
using VContainer;

namespace BattleModule.Controllers
{
    public class BattleActionController
    {
        private BattleAction _currentBattleAction;

        private Character _characterToHaveTurn;
        
        public event Action<BattleActionContext> OnBattleActionChanged;
        
        
        [Inject]
        public BattleActionController(BattleTurnController battleTurnController, 
            BattleInput battleInput)
        {
            battleTurnController.OnCharactersInTurnChanged += OnCharactersInTurnChanged;
            
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

        private void OnCharactersInTurnChanged(BattleTurnContext battleTurnContext) 
        {
            _characterToHaveTurn = battleTurnContext.CharacterInAction;

            SetDefaultBattleAction();
        }
        
        private void OnCancelButtonPressed()
        {
            if (_currentBattleAction is BattleDefaultAction)
            {
                return;
            }
            
            SetDefaultBattleAction();
        }
    }
}
