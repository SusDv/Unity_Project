using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Utility.DamageCalculator;
using CharacterModule.Inventory.Items;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.StatModifier;

namespace BattleModule.Actions.BattleActions.Processors
{
    public class ItemActionProcessor : ActionProcessor
    {
        private readonly ConsumableItem _consumableItem;
        
        public ItemActionProcessor(int sourceId, StatModifiers statModifiers, 
            ConsumableItem consumableItem) 
            : base(sourceId, statModifiers)
        {
            _consumableItem = consumableItem;
        }

        public override void ApplyModifiers(StatManager target,
            BattleActionOutcome battleActionOutcome, BattleDamage battleDamage)
        {
            _consumableItem.OnConsumableUsed?.Invoke(_consumableItem);
            
            if (!battleActionOutcome.Success)
            {
                return;
            }

            ApplyModifiers(target);
            
            ApplyTemporaryModifiers(target);
        }
    }
}