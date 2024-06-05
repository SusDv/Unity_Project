using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions.BattleActions.Interfaces;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Actions.BattleActions.Transformer;
using BattleModule.Actions.BattleActions.Transformer.Transformers;
using BattleModule.Controllers.Modules;
using BattleModule.Utility.DamageCalculator;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.Modifiers;
using CharacterModule.Stats.StatModifier;
using CharacterModule.Utility;

namespace BattleModule.Actions.BattleActions.Processors
{
    public abstract class ActionProcessor : IAction
    {
        protected StatModifiers TargetModifiers { get; }

        protected OutcomeTransformers OutcomeTransformers { get; }

        protected ActionProcessor(int sourceID,
            StatModifiers statModifiers, 
            OutcomeTransformers outcomeTransformers)
        {
            TargetModifiers = statModifiers;
            
            OutcomeTransformers = outcomeTransformers;
            
            TargetModifiers.SetSourceID(sourceID);
        }

        public abstract (List<OutcomeTransformer> toAdd, BattleActionOutcome result) ApplyModifiers(StatManager target,
            BattleActionOutcome battleActionOutcome, 
            BattleDamage battleDamage,
            BattleOutcomeController battleOutcomeController);

        protected (List<OutcomeTransformer> toAdd, BattleActionOutcome result) ProcessTransformers(BattleActionOutcome battleActionOutcome, BattleOutcomeController battleOutcomeController)
        {
            var initialOutcome = battleActionOutcome;
            
            battleOutcomeController.SetOutcomeTimers(OutcomeTransformers.GetTransformers());
            
            OutcomeTransformers.GetTransformers().OfType<StaticOutcomeTransformer>().ToList().ForEach(o =>
            {
                initialOutcome = o.TransformOutcome(initialOutcome);
            });

            return (OutcomeTransformers.GetTransformers().OfType<TemporaryOutcomeTransformer>().Cast<OutcomeTransformer>().ToList(), initialOutcome);
        }

        protected void ApplyTemporaryModifiers(StatManager target)
        {
            foreach (var modifier in TargetModifiers.GetModifiers().temporaryModifiers)
            {
                target.AddModifier(modifier);
            }
        }

        protected void ApplyModifiers(StatManager target)
        {
            foreach (var modifier in TargetModifiers.GetModifiers().modifiers)
            {
                target.AddModifier(modifier);
            }
        }

        protected void ProcessDamageModifiers(StatManager target, 
            BattleActionOutcome battleActionOutcome,
            BattleDamage battleDamage)
        {
            var damageModifier = TargetModifiers.GetModifiers().
                modifiers
                .FirstOrDefault(m => m is InstantStatModifier
            {
                IsNegative: true, 
                Type: StatType.HEALTH
            });

            if (damageModifier != default)
            {
                battleDamage.SetDamageSource(damageModifier.ModifierData.Value);
                
                damageModifier.ModifierData.Value =
                    battleDamage.CalculateAttackDamage(target, battleActionOutcome.DamageMultiplier);
                
                target.AddModifier(damageModifier);
                
                return;
            }
            
            target.AddModifier(InstantStatModifier.GetInstance(StatType.HEALTH, battleDamage.CalculateAttackDamage(target, battleActionOutcome.DamageMultiplier)));
        }
    }
}