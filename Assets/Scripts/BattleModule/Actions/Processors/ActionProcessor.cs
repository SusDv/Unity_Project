using System.Linq;
using BattleModule.Actions.Interfaces;
using BattleModule.Actions.Outcome;
using BattleModule.Actions.Transformer.Transformers;
using BattleModule.Controllers.Modules;
using BattleModule.Utility.DamageCalculator;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.Modifiers;
using CharacterModule.Stats.StatModifier;
using CharacterModule.Types.Base;
using CharacterModule.Utility;

namespace BattleModule.Actions.Processors
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

        public abstract BattleActionOutcome ProcessAction(Character target,
            BattleActionOutcome battleActionOutcome, 
            BattleDamage battleDamage,
            BattleOutcomeController battleOutcomeController);
        

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
            var damageSource = GetExternalDamageSource();

            if (DamageSourceFound(target, battleActionOutcome, battleDamage, damageSource))
            {
                return;
            }

            target.AddModifier(InstantStatModifier.GetInstance(StatType.HEALTH, battleDamage.CalculateAttackDamage(target, battleActionOutcome.DamageMultiplier)));
        }

        private static bool DamageSourceFound(StatManager target, BattleActionOutcome battleActionOutcome,
            BattleDamage battleDamage, IModifier<StatType> damageSource)
        {
            if (damageSource == default)
            {
                return false;
            }
            
            battleDamage.SetDamageSource(damageSource.ModifierData.Value);
                
            damageSource.ModifierData.Value =
                battleDamage.CalculateAttackDamage(target, battleActionOutcome.DamageMultiplier);
                
            target.AddModifier(damageSource);
                
            return true;
        }

        private IModifier<StatType> GetExternalDamageSource()
        {
            return TargetModifiers.GetModifiers().modifiers
                .FirstOrDefault(m => m is InstantStatModifier
                {
                    IsNegative: true, 
                    Type: StatType.HEALTH
                });
        }
    }
}