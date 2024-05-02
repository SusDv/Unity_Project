using BattleModule.Actions.BattleActions.Interfaces;
using BattleModule.Utility;
using CharacterModule.Stats.StatModifier;
using CharacterModule.WeaponSpecial.Interfaces;
using UnityEngine;
using Utility.Constants;
using Utility.Information;

namespace CharacterModule.WeaponSpecial.Base
{
    [CreateAssetMenu(fileName = "Special Attack", menuName = "Character/Special")]
    public class SpecialAttack : ScriptableObject, IObjectInformation, IBattleObject
    {
        [field: SerializeField] 
        public ObjectInformation ObjectInformation { get; private set; }
        
        [field: SerializeField, Header("Targeting Data"), Space]
        public TargetType TargetType { get; private set; }
        
        [field: SerializeField]
        public TargetSearchType TargetSearchType { get; private set; }
        
        [field: SerializeField]
        [field: Range(BattleTargetingConstants.SpellMin, BattleTargetingConstants.SpellMax)]
        public int MaxTargetsCount { get; private set; }
        
        [field: SerializeField, Header("Battle Action Data"), Space]
        public float BattlePoints { get; private set; }
        
        [field: SerializeField, Header("Special Attack Data"), Space]
        public float MaxEnergy { get; private set; }

        [field: SerializeReference, Space]
        public StatModifiers TargetModifiers { get; private set; } = new DynamicStatModifiers();
        
        public ISpecialAttack GetAttack()
        {
            return new DefaultSpecialAttack(MaxEnergy, TargetModifiers.GetModifiers());
        }
    }
}