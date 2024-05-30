using BattleModule.Utility;
using BattleModule.Actions.BattleActions.Interfaces;
using BattleModule.Actions.BattleActions.Processors;
using CharacterModule.Stats.StatModifier;
using CharacterModule.WeaponSpecial.Base;
using UnityEngine;

namespace CharacterModule.Inventory.Items.Equipment
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Character/Items/Equipment/Weapon")]
    public class Weapon : EquipmentItem, IBattleObject, IActionProvider
    {
        [field: SerializeField]
        public float BattlePoints { get; private set; }
        
        [field: SerializeField]
        public TargetType TargetType { get; private set; }
        
        [field: SerializeField]
        public SpecialAttack SpecialAttack { get; private set; }

        [field: SerializeReference]
        public StatModifiers HitModifiers { get; private set; } = new DynamicStatModifiers();

        public TargetSearchType TargetSearchType => TargetSearchType.SEQUENCE;
        
        public int MaxTargetsCount => 1;

        public IAction GetAction() => new DefaultActionProcessor(ID, HitModifiers);
    }
}