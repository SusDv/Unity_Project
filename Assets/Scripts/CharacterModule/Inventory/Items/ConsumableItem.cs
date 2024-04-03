using System;
using BattleModule.Utility;
using BattleModule.Utility.Interfaces;
using CharacterModule.Inventory.Interfaces;
using CharacterModule.Inventory.Items.Base;
using CharacterModule.Stats.Managers;
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
            foreach (var modifier in StatModifiers.GetModifiers())
            {
                characterStatManager.ApplyStatModifier(modifier);
            }

            OnConsumableUsed?.Invoke(this);
        }
    }
}