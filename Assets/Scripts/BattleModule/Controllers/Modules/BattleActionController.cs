using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BattleModule.Actions;
using BattleModule.Actions.BattleActions.Base;
using BattleModule.Actions.BattleActions.Context;
using BattleModule.Actions.BattleActions.Types;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.Input;
using BattleModule.Utility;
using BattleModule.Utility.Interfaces;
using CharacterModule.CharacterType.Base;
using Cysharp.Threading.Tasks;
using VContainer;

namespace BattleModule.Controllers.Modules
{
    public class BattleActionController : IBattleCancelable, ILoadingUnit
    {
        private readonly BattleInput _battleInput;
        
        private readonly BattleTurnController _battleTurnController;
        
        private readonly BattleAccuracyController _battleAccuracyController;
        
        private readonly BattleEventManager _battleEventManager;
        
        
        private BattleAction _currentBattleAction;

        private Character _characterToHaveTurn;
        
        [Inject]
        private BattleActionController(BattleInput battleInput,
            BattleTurnController battleTurnController,
            BattleAccuracyController battleAccuracyController,
            BattleEventManager battleEventManager)
        {
            _battleInput = battleInput;

            _battleTurnController = battleTurnController;
            
            _battleAccuracyController = battleAccuracyController;
            
            _battleEventManager = battleEventManager;
        }
        
        public event Action<BattleActionContext> OnBattleActionChanged = delegate { };

        public event Action OnBattleActionInvoked = delegate { };
        
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
        
        public UniTask Load()
        {
            _battleTurnController.OnCharactersInTurnChanged += OnCharactersInTurnChanged;
            
            _battleInput.PrependCancelable(this);

            return UniTask.CompletedTask;
        }
        
        public void SetDefaultBattleAction() 
        {
            SetBattleAction<DefaultAction>(_characterToHaveTurn.WeaponController.GetWeapon());
        }

        public Action<object> GetInvokeAction()
        {
            return _ => OnBattleActionInvoked?.Invoke();
        }

        private void OnCharactersInTurnChanged(BattleTurnContext battleTurnContext) 
        {
            _characterToHaveTurn = battleTurnContext.CharacterInAction;
        }
    }
}
