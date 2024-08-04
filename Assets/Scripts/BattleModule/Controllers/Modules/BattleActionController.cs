using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions.Base;
using BattleModule.Actions.Context;
using BattleModule.Actions.Interfaces;
using BattleModule.Actions.Outcome;
using BattleModule.Actions.Types;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.Utility.Interfaces;
using CharacterModule.Animation;
using CharacterModule.Stats.Managers;
using CharacterModule.Types.Base;
using CharacterModule.WeaponSpecial.Interfaces;
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

        private ActionData _actionData;

        public event Action OnBattleActionStarted = delegate { };

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
                _currentBattleAction.Init(actionObject, _actionData));
        }

        public async UniTask ExecuteBattleAction(List<Character> targets)
        {
            OnBattleActionStarted?.Invoke();
            
            var operation = await _currentBattleAction.PerformAction(targets, _battleOutcomeController);
            
            if (!operation.status)
            {
                return;
            }

            OnBattleActionFinished?.Invoke(targets, operation.result);
        }

        public bool TryCancel()
        {
            if (_currentBattleAction is DefaultAction)
            {
                return false;
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
            SetBattleAction<DefaultAction>(_actionData.DefaultBattleObject);
        }

        private void OnCharactersInTurnChanged(BattleTurnContext battleTurnContext)
        {
            var characterInTurn = battleTurnContext.CharactersInTurn.First();
            
            _actionData = new ActionData()
            {
                CharacterType = characterInTurn.GetType(),
                
                DefaultBattleObject = characterInTurn.EquipmentController.WeaponController.GetWeapon(),
                
                SpecialAttack = characterInTurn.EquipmentController.WeaponController.GetSpecialAttack(),
                
                StatsController = characterInTurn.Stats,
                
                AnimationManager = characterInTurn.AnimationManager
            };
        }

        public struct ActionData
        {
            public Type CharacterType;
            
            public IBattleObject DefaultBattleObject;

            public ISpecialAttack SpecialAttack;

            public StatsController StatsController;

            public AnimationManager AnimationManager;
        }
    }
}
