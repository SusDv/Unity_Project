using System.Linq;
using BattleModule.Actions.BattleActions.Interfaces;
using BattleModule.Actions.BattleActions.Outcome;
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

        protected ActionProcessor(int sourceID, StatModifiers statModifiers)
        {
            TargetModifiers = statModifiers;
            
            TargetModifiers.SetSourceID(sourceID);
        }

        public abstract void ApplyModifiers(StatManager target,
            BattleActionOutcome battleActionOutcome, BattleDamage battleDamage);

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
            BattleActionOutcome battleActionOutcome, BattleDamage battleDamage)
        {
            var damageModifier = TargetModifiers.GetModifiers().modifiers.FirstOrDefault(m => m is InstantStatModifier
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