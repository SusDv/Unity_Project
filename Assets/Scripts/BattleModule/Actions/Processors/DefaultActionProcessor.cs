using System.Linq;
using BattleModule.Actions.Outcome;
using BattleModule.Actions.Transformer.Transformers;
using BattleModule.Controllers.Modules;
using BattleModule.Utility.DamageCalculator;
using CharacterModule.Stats.Modifiers;
using CharacterModule.Stats.Modifiers.Containers;
using CharacterModule.Types.Base;

namespace BattleModule.Actions.Processors
{
    public class DefaultActionProcessor : ActionProcessor
    {
        public DefaultActionProcessor(int sourceID,
            StatModifiers statModifiers,
            OutcomeTransformers outcomeTransformers) 
            : base(sourceID, statModifiers, outcomeTransformers)
        { }
        
        public override BattleActionOutcome ProcessAction(Character target,
            BattleActionOutcome battleActionOutcome, 
            BattleDamage battleDamage,
            BattleOutcomeController battleOutcomeController)
        {
            var transformedOutcome = battleOutcomeController.ProcessHitTransformers(target, battleActionOutcome, OutcomeTransformers.GetTransformers());
            
            ProcessDamageModifiers(target.Stats, transformedOutcome, battleDamage);
            
            foreach (var modifier in TargetModifiers.GetModifiers().modifiers.Where(m => m is not InstantStatModifier))
            {
                target.Stats.AddModifier(modifier);
            }
            
            ApplyTemporaryModifiers(target.Stats);

            return transformedOutcome;
        }
    }
}