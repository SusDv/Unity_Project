using System.Collections.Generic;
using System.Linq;
using CharacterModule.Stats.Managers;
using CharacterModule.Types.Base;
using CharacterModule.Utility;
using BattleModule.Accuracy;
using BattleModule.Actions.BattleActions.Context;
using BattleModule.Actions.BattleActions.Interfaces;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Utility.DamageCalculator;
using Cysharp.Threading.Tasks;

namespace BattleModule.Actions.BattleActions.Base
{
    public abstract class BattleAction
    {
        private BattleActionContext _battleActionContext;

        private IAction _actionObject;

        private BattleDamage _battleDamage;

        protected abstract string ActionAnimationName { get; }

        public BattleActionContext Init(object actionObject, 
            Character character)
        {
            _battleActionContext = new BattleActionContext(actionObject, character.GetType());

            _actionObject = (actionObject as IActionProvider)?.GetAction();

            _battleDamage = GetDamageCalculator(character.Stats);

            return _battleActionContext;
        }
        
        private void ApplyHitModifiers(Character source, IReadOnlyList<Character> targets,
            IReadOnlyList<BattleActionOutcome> accuracyResult)
        {
            for (var i = 0; i < accuracyResult.Count; i++)
            {
                _actionObject.ApplyModifiers(targets[i].Stats, 
                    accuracyResult[i], _battleDamage);
            }
            
            source.EquipmentController.WeaponController.GetSpecialAttack().Charge(5f);
            
            source.Stats.ApplyInstantModifier(StatType.BATTLE_POINTS, _battleActionContext.BattleObject.BattlePoints);
        }

        private IReadOnlyList<BattleActionOutcome> EvaluateAccuracies(
            IEnumerable<Character> targets,
            IReadOnlyDictionary<Character, BattleAccuracy> accuracies)
        {
            var accuracyResult = new List<BattleActionOutcome>();
            
            foreach (var target in targets)
            {
                var initialOutcome = accuracies[target].Evaluate();

                initialOutcome = target.EquipmentController.GetTransformers().Aggregate(initialOutcome, (current, transformer) => transformer.TransformOutcome(accuracies[target], current));

                accuracyResult.Add(initialOutcome);
            }

            return accuracyResult;
        }
        
        protected virtual async UniTask PlayActionAnimation(Character source, List<Character> targets)
        {
            await source.AnimationManager.PlayAnimation(ActionAnimationName);
        }
        
        public async UniTask<IReadOnlyList<BattleActionOutcome>> PerformAction(Character source,
            List<Character> targets,
            Dictionary<Character, BattleAccuracy> accuracies)
        {
            var accuracyResult = EvaluateAccuracies(targets, accuracies);
            
            await PlayActionAnimation(source, targets);
            
            ApplyHitModifiers(source, targets, accuracyResult);
            
            return accuracyResult;
        }

        protected virtual BattleDamage GetDamageCalculator(StatManager statManager)
        {
            return new PhysicalDamage(statManager);
        }
    }
}
