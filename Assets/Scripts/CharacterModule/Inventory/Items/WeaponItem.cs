using BattleModule.Utility.Enums;
using BattleModule.Utility.Interfaces;
using CharacterModule.Inventory.Interfaces;
using CharacterModule.Stats.Managers;
using UnityEngine;

namespace CharacterModule.Inventory.Items
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Character/Items/Equipment/Weapon")]
    public class WeaponItem : EquipmentItem, IEquipment, ITargeting 
    {
        [field: SerializeField]
        public TargetType TargetType { get; set; }

        [field: SerializeField]
        public TargetSearchType TargetSearchType { get; set; }

        [field: SerializeField]
        [field: Range(1, 5)]
        public int MaxTargetsCount { get; set; } = 1;

        public override void Equip(StatManager stats)
        {
            foreach (var baseStatModifier in StatModifiers.GetModifiers())
            {
                stats.ApplyStatModifier(baseStatModifier);
            }
        }

        public override void Unequip(StatManager stats)
        {
            stats.RemoveStatModifiersByCondition((statModifier) => statModifier.SourceID == ID);
        }
    }
}