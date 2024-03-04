using System;
using BattleModule.Utility.Enums;
using BattleModule.Utility.Interfaces;
using CharacterModule.Inventory.Items.Base;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
using InventorySystem.Item;
using InventorySystem.Item.Interfaces;
using UnityEngine;

namespace CharacterModule.Inventory.Items 
{
    [CreateAssetMenu(fileName = "New Consumable", menuName = "Character/Items/Consumable")]
    public class ConsumableItem : ItemBase, IConsumable, ITargeting
    {
        [field: SerializeField]
        public TargetType TargetType { get; set; }

        [field: SerializeField]
        public TargetSearchType TargetSearchType { get; set; }

        [field: SerializeField]
        [field: Range(1, 5)]
        public int MaxTargetsCount { get; set; } = 1;

        public event Action<ItemBase> OnConsumableUsed;

        public void Consume(StatManager characterStatManager)
        {
            foreach (var modifier in StatModifiers.BaseModifiers)
            {
                characterStatManager.ApplyStatModifier(modifier.Clone() as BaseStatModifier);
            }

            OnConsumableUsed?.Invoke(this);
        }
    }
}