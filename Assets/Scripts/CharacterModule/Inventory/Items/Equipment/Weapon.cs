using BattleModule.Actions.Interfaces;
using BattleModule.Actions.Processors;
using BattleModule.Actions.Transformer.Transformers;
using BattleModule.Utility;
using CharacterModule.Stats.StatModifier;
using CharacterModule.WeaponSpecial.Base;
using UnityEngine;

namespace CharacterModule.Inventory.Items.Equipment
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Character/Items/Equipment/Weapon")]
    public class Weapon : EquipmentItem, IBattleObject, IActionProvider
    {
        [field: SerializeField] 
        public HybridTransformers OutcomeTransformers;
        
        [field: SerializeField]
        public float BattlePoints { get; private set; }
        
        [field: SerializeField]
        public TargetType TargetType { get; private set; }
        
        [field: SerializeField]
        public SpecialAttack SpecialAttack { get; private set; }

        [field: SerializeField]
        public DynamicStatModifiers HitModifiers { get; private set; }

        public TargetSearchType TargetSearchType => TargetSearchType.SEQUENCE;
        
        public int MaxTargetsCount => 1;

        public IAction GetAction() => new DefaultActionProcessor(ID, HitModifiers, OutcomeTransformers);
    }
}