using BattleModule.Utility.Enums;
using BattleModule.Utility.Interfaces;
using InventorySystem.Item.Interfaces;
using StatModule.Base;
using StatModule.Interfaces;
using StatModule.Modifier;
using System;
using UnityEngine;

namespace InventorySystem.Item 
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

        public void Consume(IHaveStats character)
        {
            foreach (BaseStatModifier modifier in StatModifiers.BaseModifiers)
            {
                character.AddStatModifier(modifier.Clone() as BaseStatModifier);
            }

            OnConsumableUsed?.Invoke(this);
        }
    }
}