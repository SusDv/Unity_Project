using Utility.Constants;
using Utility.Information;
using BattleModule.Utility;
using BattleModule.Actions.BattleActions.Interfaces;
using BattleModule.Actions.BattleActions.Transformer.Transformers;
using UnityEngine;

namespace CharacterModule.Spells.Core
{
    public abstract class SpellBase : ScriptableObject, IObjectInformation, IActionProvider, IBattleObject
    {
        [field: SerializeField] 
        public ObjectInformation ObjectInformation { get; set; }
        
        [field: SerializeField]
        public float BattlePoints { get; set; }
        
        [field: SerializeField]
        public TargetType TargetType { get; set; }
        
        [field: SerializeField]
        public TargetSearchType TargetSearchType { get; set; }

        [field: SerializeField]
        [field: Range(BattleTargetingConstants.SpellMin, BattleTargetingConstants.SpellMax)]
        public int MaxTargetsCount { get; set; }
        
        [field: SerializeReference] 
        protected OutcomeTransformers OutcomeTransformers = new DynamicTransformers();

        public abstract IAction GetAction();
    }
}
