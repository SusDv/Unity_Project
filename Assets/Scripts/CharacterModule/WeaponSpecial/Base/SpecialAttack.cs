using BattleModule.Actions.Interfaces;
using BattleModule.Actions.Processors;
using BattleModule.Actions.Transformer.Transformers;
using BattleModule.Utility;
using CharacterModule.Stats.Modifiers.Containers;
using CharacterModule.WeaponSpecial.Interfaces;
using UnityEngine;
using Utility.Constants;
using Utility.Information;

namespace CharacterModule.WeaponSpecial.Base
{
    [CreateAssetMenu(fileName = "Special Attack", menuName = "Character/Special")]
    public class SpecialAttack : ScriptableObject, IObjectInformation, IBattleObject, IActionProvider
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

        [field: SerializeField, Space]
        public DynamicStatModifiers TargetModifiers { get; private set; }
        
        [field: SerializeField] 
        private HybridTransformers _outcomeTransformers;
        
        public ISpecialAttack GetAttack()
        {
            return new DefaultSpecialAttack(MaxEnergy);
        }

        public IAction GetAction()
        {
            return new SpecialActionProcessor(GetInstanceID(), 
                TargetModifiers, GetAttack(), _outcomeTransformers);
        }
    }
}