using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions.BattleActions.Base;
using BattleModule.Actions.BattleActions.Context;
using BattleModule.Actions.BattleActions.Types;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.Utility;
using BattleModule.Utility.Interfaces;
using CharacterModule.Types.Base;
using Cysharp.Threading.Tasks;
using VContainer;

namespace BattleModule.Controllers.Modules
{
    public class BattleActionController : IBattleCancelable, ILoadingUnit
    {
        private readonly BattleCancelableController _battleCancelableController;
        
        private readonly BattleTurnController _battleTurnController;
        
        private readonly BattleAccuracyController _battleAccuracyController;
        
        private BattleAction _currentBattleAction;

        private Character _characterToHaveTurn;
        
        public event Action<BattleActionContext> OnBattleActionChanged = delegate { };
        
        [Inject]
        private BattleActionController(BattleCancelableController battleCancelableController,
            BattleTurnController battleTurnController,
            BattleAccuracyController battleAccuracyController)
        {
            _battleCancelableController = battleCancelableController;

            _battleTurnController = battleTurnController;
            
            _battleAccuracyController = battleAccuracyController;
        }
        
        public void SetBattleAction<T>(object actionObject)
            where T : BattleAction
        {
            _currentBattleAction = Activator.CreateInstance<T>();

            OnBattleActionChanged?.Invoke(
                _currentBattleAction.Init(actionObject, _characterToHaveTurn));
        }

        public void ExecuteBattleAction(List<Character> targets)
        {
            _currentBattleAction.PerformAction(
                _characterToHaveTurn, 
                targets, 
                _battleAccuracyController.GetAccuracies());
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
            
            _battleCancelableController.PrependCancelable(this);

            return UniTask.CompletedTask;
        }
        
        public void SetDefaultBattleAction() 
        {
            SetBattleAction<DefaultAction>(_characterToHaveTurn.EquipmentController.WeaponController.GetWeapon());
        }

        private void OnCharactersInTurnChanged(BattleTurnContext battleTurnContext) 
        {
            _characterToHaveTurn = battleTurnContext.CharactersInTurn.First();
        }
    }
}
