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
    public class ConsumableItem : ItemBase, IConsumable, ITargetableObject
    {
        [field: SerializeField]
        public TargetType TargetType { get; set; }

        public TargetSearchType TargetSearchType { get; set; } = TargetSearchType.SEQUENCE;
        
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