using BattleModule.Actions.Outcome;
using BattleModule.Actions.Transformer.Transformers;
using BattleModule.Controllers.Modules;
using BattleModule.Utility.DamageCalculator;
using CharacterModule.Inventory.Items;
using CharacterModule.Stats.Modifiers.Containers;
using CharacterModule.Types.Base;

namespace BattleModule.Actions.Processors
{
    public class ItemActionProcessor : ActionProcessor
    {
        private readonly ConsumableItem _consumableItem;
        
        public ItemActionProcessor(int sourceId, 
            StatModifiers statModifiers, 
            ConsumableItem consumableItem,
            OutcomeTransformers outcomeTransformers) 
            : base(sourceId, statModifiers, outcomeTransformers)
        {
            _consumableItem = consumableItem;
        }

        public override BattleActionOutcome ProcessAction(Character target,
            BattleActionOutcome battleActionOutcome, 
            BattleDamage battleDamage,
            BattleOutcomeController battleOutcomeController)
        {
            var processed = battleOutcomeController.ProcessHitTransformers(target, battleActionOutcome, OutcomeTransformers.GetTransformers());
            
            _consumableItem.OnConsumableUsed?.Invoke(_consumableItem);
            
            if (!processed.Success)
            {
                return processed;
            }

            ApplyModifiers(target.Stats);
            
            ApplyTemporaryModifiers(target.Stats);

            return processed;
        }
    }
}