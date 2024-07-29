using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions.Base;
using BattleModule.Actions.Context;
using BattleModule.Actions.Outcome;
using BattleModule.Actions.Types;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.Utility.Interfaces;
using CharacterModule.Types.Base;
using Cysharp.Threading.Tasks;
using Utility;
using VContainer;

namespace BattleModule.Controllers.Modules
{
    public class BattleActionController : IBattleCancelable, ILoadingUnit
    {
        private readonly BattleCancelableController _battleCancelableController;
        
        private readonly BattleTurnController _battleTurnController;

        private readonly BattleOutcomeController _battleOutcomeController;
        
        private BattleAction _currentBattleAction;

        private Character _characterToHaveTurn;
        
        public event Action<BattleActionContext> OnBattleActionChanged = delegate { };

        public event Action<List<Character>, IReadOnlyList<BattleActionOutcome>> OnBattleActionFinished = delegate { };

        [Inject]
        private BattleActionController(BattleCancelableController battleCancelableController,
            BattleTurnController battleTurnController,
            BattleOutcomeController battleOutcomeController)
        {
            _battleCancelableController = battleCancelableController;

            _battleTurnController = battleTurnController;
            
            _battleOutcomeController = battleOutcomeController;
        }
        
        public void SetBattleAction<T>(object actionObject)
            where T : BattleAction
        {
            _currentBattleAction = Activator.CreateInstance<T>();

            OnBattleActionChanged?.Invoke(
                _currentBattleAction.Init(actionObject, _characterToHaveTurn.Stats));
        }

        public async UniTask ExecuteBattleAction(List<Character> targets)
        {
            var operation = await _currentBattleAction.PerformAction(_characterToHaveTurn, 
                targets, _battleOutcomeController);
            
            if (!operation.status)
            {
                return;
            }

            OnBattleActionFinished?.Invoke(targets, operation.result);
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
