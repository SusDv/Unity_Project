using System.Linq;
using BattleModule.Actions.BattleActions.Interfaces;
using BattleModule.Actions.BattleActions.Outcome;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.Modifiers;
using CharacterModule.Stats.StatModifier;
using CharacterModule.Utility;

namespace BattleModule.Actions.BattleActions.Processors
{
    public class ActionProcessor : IAction
    {
        private float BattlePoints { get; }

        private StatModifiers TargetModifiers { get; }

        public ActionProcessor(float battlePoints, 
            StatModifiers statModifiers)
        {
            BattlePoints = battlePoints;

            TargetModifiers = statModifiers;
        }

        public virtual void ApplyModifiers(StatManager source, 
            StatManager target, BattleActionOutcome battleActionOutcome)
        {
            ProcessDamageModifiers(target, battleActionOutcome);
            
            foreach (var modifier in TargetModifiers.GetModifiers().modifiers.Where(m => m is not InstantStatModifier))
            {
                target.AddModifier(modifier);
            }
            
            ApplyTemporaryModifiers(target);
            
            AddBattlePoints(source);
        }

        protected void AddBattlePoints(StatManager source)
        {
            source.ApplyInstantModifier(StatType.BATTLE_POINTS, BattlePoints);
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

        protected void ProcessDamageModifiers(StatManager target, BattleActionOutcome battleActionOutcome)
        {
            foreach (var modifier in TargetModifiers.GetModifiers().modifiers.Where(m => m is InstantStatModifier { IsNegative: true, Type: StatType.HEALTH}))
            {
                modifier.ModifierData.Value *= battleActionOutcome.DamageMultiplier;
                
                target.AddModifier(modifier);
            }
        }
    }
}