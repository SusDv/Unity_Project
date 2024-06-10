using BattleModule.Actions.Outcome;
using BattleModule.Actions.Transformer.Transformers;
using BattleModule.Controllers.Modules;
using BattleModule.Utility.DamageCalculator;
using CharacterModule.Stats.StatModifier;
using CharacterModule.Types.Base;
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

        public override BattleActionOutcome ProcessAction(Character target, 
            BattleActionOutcome battleActionOutcome, 
            BattleDamage battleDamage,
            BattleOutcomeController battleOutcomeController)
        {
            var processed = battleOutcomeController.ProcessHitTransformers(target, battleActionOutcome, OutcomeTransformers.GetTransformers());
            
            if (!_specialAttack.IsReady())
            {
                return processed;
            }
            
            ProcessDamageModifiers(target.Stats, processed, battleDamage);
            
            ApplyTemporaryModifiers(target.Stats);

            return processed;
        }
    }
}