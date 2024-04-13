using Utility;
using Utility.Constants;
using Utility.Information;
using BattleModule.Utility;
using BattleModule.Actions.BattleActions.Interfaces;
using CharacterModule.CharacterType.Base;
using CharacterModule.Spells.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterModule.Spells.Core
{
    public abstract class SpellBase : ScriptableObject, IObjectInformation, ISpell, IBattleObject
    {
        [field: SerializeField] 
        public ObjectInformation Information { get; set; }
        
        [field: SerializeField]
        public float BattlePoints { get; set; }
        
        [field: SerializeField]
        public TargetType TargetType { get; set; }
        
        [field: SerializeField]
        public TargetSearchType TargetSearchType { get; set; }

        [field: SerializeField]
        [field: Range(BattleTargetingConstants.SpellMin, BattleTargetingConstants.SpellMax)]
        public int MaxTargetsCount { get; set; }

        public abstract void UseSpell(List<Character> targets);
    }
}
