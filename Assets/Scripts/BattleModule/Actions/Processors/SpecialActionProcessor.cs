using System.Collections.Generic;
using BattleModule.Actions.Outcome;
using BattleModule.Actions.Transformer;
using BattleModule.Actions.Transformer.Transformers;
using BattleModule.Controllers.Modules;
using BattleModule.Utility.DamageCalculator;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.StatModifier;
using CharacterModule.WeaponSpecial.Interfaces;

namespace BattleModule.Actions.Processors
{
    public class SpecialActionProcessor : ActionProcessor
    {
        private readonly ISpecialAttack _specialAttack;
        
        public SpecialActionProcessor(int sourceID, 
            StatModifiers statModifiers,
            ISpecialAttack specialAttack,
            OutcomeTransformers outcomeTransformers) 
            : base(sourceID, statModifiers, outcomeTransformers)
        {
            _specialAttack = specialAttack;
        }

        public override (List<OutcomeTransformer> toAdd, BattleActionOutcome result) ProcessAction(StatManager target, 
            BattleActionOutcome battleActionOutcome, 
            BattleDamage battleDamage,
            BattleOutcomeController battleOutcomeController)
        {
            var processed = battleOutcomeController.ProcessHitTransformers(battleActionOutcome, OutcomeTransformers.GetTransformers());
            
            if (!_specialAttack.IsReady())
            {
                return processed;
            }
            
            ProcessDamageModifiers(target, processed.result, battleDamage);
            
            ApplyTemporaryModifiers(target);

            return processed;
        }
    }
}