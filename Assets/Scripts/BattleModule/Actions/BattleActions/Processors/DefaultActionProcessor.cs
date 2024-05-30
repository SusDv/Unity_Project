using System.Linq;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Utility.DamageCalculator;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.Modifiers;
using CharacterModule.Stats.StatModifier;

namespace BattleModule.Actions.BattleActions.Processors
{
    public class DefaultActionProcessor : ActionProcessor
    {
        public DefaultActionProcessor(int sourceID, StatModifiers statModifiers) 
            : base(sourceID, statModifiers)
        { }
        
        public override void ApplyModifiers(StatManager target,
            BattleActionOutcome battleActionOutcome, BattleDamage battleDamage)
        {
            ProcessDamageModifiers(target, battleActionOutcome, battleDamage);
            
            foreach (var modifier in TargetModifiers.GetModifiers().modifiers.Where(m => m is not InstantStatModifier))
            {
                target.AddModifier(modifier);
            }
            
            ApplyTemporaryModifiers(target);
        }
    }
}