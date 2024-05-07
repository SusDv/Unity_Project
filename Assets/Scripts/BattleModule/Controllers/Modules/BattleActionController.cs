using System;
using System.Collections.Generic;
using BattleModule.Actions;
using BattleModule.Actions.BattleActions.Base;
using BattleModule.Actions.BattleActions.Context;
using BattleModule.Actions.BattleActions.Types;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.Input;
using BattleModule.Utility.Interfaces;
using CharacterModule.CharacterType.Base;
using VContainer;

namespace BattleModule.Controllers.Modules
{
    public class BattleActionController : IBattleCancelable
    {
        private readonly BattleEventManager _battleEventManager;

        private readonly BattleAccuracyController _battleAccuracyController;

        private BattleAction _currentBattleAction;

        private Character _characterToHaveTurn;
        
        public event Action<BattleActionContext> OnBattleActionChanged;
        
        [Inject]
        public BattleActionController(BattleTurnController battleTurnController, 
            BattleInput battleInput,
            BattleAccuracyController battleAccuracyController,
            BattleEventManager battleEventManager)
        {
            _battleEventManager = battleEventManager;

            _battleAccuracyController = battleAccuracyController;
            
            battleTurnController.OnCharactersInTurnChanged += OnCharactersInTurnChanged;
            
            battleInput.PrependCancelable(this);
        }
        
        private void SetDefaultBattleAction() 
        {
            SetBattleAction<DefaultAction>(_characterToHaveTurn.WeaponController.GetWeapon());
        }

        private void OnCharactersInTurnChanged(BattleTurnContext battleTurnContext) 
        {
            _characterToHaveTurn = battleTurnContext.CharacterInAction;

            SetDefaultBattleAction();
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
            _currentBattleAction.PerformAction(_characterToHaveTurn, targets, _battleEventManager.AdvanceTurn);
        }
        
        public bool Cancel()
        {
            if (_currentBattleAction is DefaultAction)
            {
                return true;
            }
            
            SetDefaultBattleAction();
            
            return true;
        }
    }
}
