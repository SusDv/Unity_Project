using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Utility.DamageCalculator;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.StatModifier;
using CharacterModule.WeaponSpecial.Interfaces;

namespace BattleModule.Actions.BattleActions.Processors
{
    public class SpecialActionProcessor : ActionProcessor
    {
        private readonly ISpecialAttack _specialAttack;
        
        public SpecialActionProcessor(int sourceID, 
            StatModifiers statModifiers,
            ISpecialAttack specialAttack) 
            : base(sourceID, statModifiers)
        {
            _specialAttack = specialAttack;
        }

        public override void ApplyModifiers(StatManager target, BattleActionOutcome battleActionOutcome, BattleDamage battleDamage)
        {
            if (!_specialAttack.IsReady())
            {
                return;
            }
            
            ProcessDamageModifiers(target, battleActionOutcome, battleDamage);
            
            ApplyTemporaryModifiers(target);
        }
    }
}