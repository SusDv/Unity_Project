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
using CharacterModule.Utility;
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

        private BattleActionContext _currentBattleActionContext;

        private ActionData _actionData;

        private List<Character> _charactersOnScene;

        public event Action OnBattleActionStarted = delegate { };

        public event Action<BattleActionContext> OnBattleActionChanged = delegate { };

        public event Action<ActionResult> OnBattleActionFinished = delegate { };

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

            _currentBattleActionContext = _currentBattleAction.Init(actionObject, _actionData);
         
            OnBattleActionChanged?.Invoke(_currentBattleActionContext);
        }

        public async UniTask ExecuteBattleAction(List<Character> targets)
        {
            OnBattleActionStarted?.Invoke();
            
            try
            {
                var outcomes = await _currentBattleAction.PerformAction(targets, _battleOutcomeController);
                
                var actionResult = GetActionResult(targets, outcomes);
                
                OnBattleActionFinished?.Invoke(actionResult);
            }
            catch 
            {
                // Battle Action interrupted;
            }
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

        private ActionResult GetActionResult(List<Character> affectedTargets,
            List<BattleActionOutcome> affectedTargetsOutcome)
        {
            return new ActionResult()
            {
                AffectedTargets = affectedTargets,
                
                AffectedTargetsOutcome = affectedTargetsOutcome
            };
        }

        private void OnCharactersInTurnChanged(BattleTurnContext battleTurnContext)
        {
            _charactersOnScene = battleTurnContext.CharactersInTurn;
            
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

        private int CalculatePositionAfterAction()
        {
            var battlePointsAfterAction = _currentBattleActionContext.BattleObject.BattlePoints;

            var indexAfterAction = -1;
            
            for (var i = 1; i < _charactersOnScene.Count; i++)
            {
                if (_charactersOnScene[i].Stats.GetStatInfo(StatType.BATTLE_POINTS).FinalValue <
                      battlePointsAfterAction)
                {
                    continue;
                }

                indexAfterAction = i - 1;
                    
                break;
            }

            return indexAfterAction < 0 ? _charactersOnScene.Count - 1 : indexAfterAction;
        }

        public struct ActionData
        {
            public Type CharacterType;
            
            public IBattleObject DefaultBattleObject;

            public ISpecialAttack SpecialAttack;

            public StatsController StatsController;

            public AnimationManager AnimationManager;
        }

        public struct ActionResult
        {
            public List<Character> AffectedTargets;

            public List<BattleActionOutcome> AffectedTargetsOutcome;
        }
    }
}
