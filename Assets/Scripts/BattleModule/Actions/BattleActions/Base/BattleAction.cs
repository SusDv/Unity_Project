using System.Collections.Generic;
using System.Linq;
using BattleModule.Accuracy;
using BattleModule.Actions.BattleActions.Context;
using BattleModule.Actions.BattleActions.Interfaces;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Utility.DamageCalculator;
using CharacterModule.Stats.Managers;
using CharacterModule.Types.Base;
using CharacterModule.Utility;

namespace BattleModule.Actions.BattleActions.Base
{
    public abstract class BattleAction
    {
        private BattleActionContext _battleActionContext;

        private IAction _actionObject;

        private BattleDamage _battleDamage;

        public BattleActionContext Init(object actionObject, 
            Character character)
        {
            _battleActionContext = new BattleActionContext(actionObject, character.GetType());

            _actionObject = (actionObject as IActionProvider)?.GetAction();

            _battleDamage = GetDamageCalculator(character.Stats);

            return _battleActionContext;
        }
        
        private void ApplyHitModifiers(Character source, List<Character> targets,
            IReadOnlyDictionary<Character, BattleActionOutcome> accuracyResult)
        {
            foreach (var target in targets)
            {
                _actionObject.ApplyModifiers(target.Stats, 
                    accuracyResult[target], _battleDamage);
            }
            
            source.EquipmentController.WeaponController.GetSpecialAttack().Charge(5f);
            
            source.Stats.ApplyInstantModifier(StatType.BATTLE_POINTS, _battleActionContext.BattleObject.BattlePoints);
        }

        private Dictionary<Character, BattleActionOutcome> EvaluateAccuracies(
            IEnumerable<Character> targets,
            IReadOnlyDictionary<Character, BattleAccuracy> accuracies)
        {
            var accuracyResult = new Dictionary<Character, BattleActionOutcome>();
            
            foreach (var target in targets)
            {
                var initialOutcome = accuracies[target].Evaluate();

                initialOutcome = target.EquipmentController.GetTransformers().Aggregate(initialOutcome, (current, transformer) => transformer.TransformOutcome(accuracies[target], current));

                accuracyResult.Add(target, initialOutcome);
            }

            return accuracyResult;
        }
        
        public virtual Dictionary<Character, BattleActionOutcome> PerformAction(Character source,
            List<Character> targets,
            Dictionary<Character, BattleAccuracy> accuracies)
        {
            var accuracyResult = EvaluateAccuracies(targets, accuracies);
            
            ApplyHitModifiers(source, targets, accuracyResult);
            
            return accuracyResult;
        }

        protected virtual BattleDamage GetDamageCalculator(StatManager statManager)
        {
            return new PhysicalDamage(statManager);
        }
    }
}
