using System.Collections.Generic;
using BattleModule.Actions.Outcome;
using BattleModule.Actions.Transformer;
using BattleModule.Actions.Transformer.Transformers;
using BattleModule.Controllers.Modules;
using BattleModule.Utility.DamageCalculator;
using CharacterModule.Inventory.Items;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.StatModifier;

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

        public override (List<OutcomeTransformer> toAdd, BattleActionOutcome result) ProcessAction(StatManager target,
            BattleActionOutcome battleActionOutcome, 
            BattleDamage battleDamage,
            BattleOutcomeController battleOutcomeController)
        {
            var processed = ProcessTransformers(battleActionOutcome, battleOutcomeController);
            
            _consumableItem.OnConsumableUsed?.Invoke(_consumableItem);
            
            if (!processed.result.Success)
            {
                return processed;
            }

            ApplyModifiers(target);
            
            ApplyTemporaryModifiers(target);

            return processed;
        }
    }
}