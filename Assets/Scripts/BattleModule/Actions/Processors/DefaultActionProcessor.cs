using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions.Outcome;
using BattleModule.Actions.Transformer;
using BattleModule.Actions.Transformer.Transformers;
using BattleModule.Controllers.Modules;
using BattleModule.Utility.DamageCalculator;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.Modifiers;
using CharacterModule.Stats.StatModifier;

namespace BattleModule.Actions.Processors
{
    public class DefaultActionProcessor : ActionProcessor
    {
        public DefaultActionProcessor(int sourceID,
            StatModifiers statModifiers,
            OutcomeTransformers outcomeTransformers) 
            : base(sourceID, statModifiers, outcomeTransformers)
        { }
        
        public override (List<OutcomeTransformer> toAdd, BattleActionOutcome result) ProcessAction(StatManager target,
            BattleActionOutcome battleActionOutcome, 
            BattleDamage battleDamage,
            BattleOutcomeController battleOutcomeController)
        {
            var processed = battleOutcomeController.ProcessHitTransformers(battleActionOutcome, OutcomeTransformers.GetTransformers());
            
            ProcessDamageModifiers(target, processed.result, battleDamage);
            
            foreach (var modifier in TargetModifiers.GetModifiers().modifiers.Where(m => m is not InstantStatModifier))
            {
                target.AddModifier(modifier);
            }
            
            ApplyTemporaryModifiers(target);

            return processed;
        }
    }
}