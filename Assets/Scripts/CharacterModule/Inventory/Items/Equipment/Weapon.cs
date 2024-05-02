using BattleModule.Utility;
using BattleModule.Actions.BattleActions.Interfaces;
using CharacterModule.WeaponSpecial.Base;
using UnityEngine;

namespace CharacterModule.Inventory.Items.Equipment
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Character/Items/Equipment/Weapon")]
    public class Weapon : EquipmentItem, IBattleObject
    {
        [field: SerializeField]
        public float BattlePoints { get; private set; }
        
        [field: SerializeField]
        public TargetType TargetType { get; private set; }
        
        [field: SerializeField]
        public SpecialAttack SpecialAttack { get; private set; }

        public TargetSearchType TargetSearchType => TargetSearchType.SEQUENCE;
        
        public int MaxTargetsCount => 1;
    }
}