using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Actions.BattleActions.Transformer;
using BattleModule.Actions.BattleActions.Transformer.Transformers;
using BattleModule.Controllers.Modules;
using BattleModule.Utility.DamageCalculator;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.Modifiers;
using CharacterModule.Stats.StatModifier;

namespace BattleModule.Actions.BattleActions.Processors
{
    public class DefaultActionProcessor : ActionProcessor
    {
        public DefaultActionProcessor(int sourceID,
            StatModifiers statModifiers,
            OutcomeTransformers outcomeTransformers) 
            : base(sourceID, statModifiers, outcomeTransformers)
        { }
        
        public override (List<OutcomeTransformer> toAdd, BattleActionOutcome result) ApplyModifiers(StatManager target,
            BattleActionOutcome battleActionOutcome, 
            BattleDamage battleDamage,
            BattleOutcomeController battleOutcomeController)
        {
            var processed = ProcessTransformers(battleActionOutcome, battleOutcomeController);
            
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