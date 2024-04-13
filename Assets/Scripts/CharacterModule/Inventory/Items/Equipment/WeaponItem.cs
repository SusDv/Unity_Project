using BattleModule.Utility;
using BattleModule.Actions.BattleActions.Interfaces;
using CharacterModule.WeaponSpecial.Base;
using UnityEngine;

namespace CharacterModule.Inventory.Items.Equipment
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Character/Items/Equipment/Weapon")]
    public class WeaponItem : EquipmentItem, IBattleObject
    {
        [field: SerializeField]
        public float BattlePoints { get; set; }
        
        [field: SerializeField]
        public TargetType TargetType { get; set; }
        
        [field: SerializeField]
        public SpecialAttack SpecialAttack { get; private set; }

        public TargetSearchType TargetSearchType { get; set; } = TargetSearchType.SEQUENCE;
        
        public int MaxTargetsCount { get; set; } = 1;
    }
}