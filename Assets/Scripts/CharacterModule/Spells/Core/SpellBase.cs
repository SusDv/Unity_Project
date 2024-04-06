using System.Collections.Generic;
using BattleModule.Utility;
using BattleModule.Utility.Interfaces;
using CharacterModule.Spells.Interfaces;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.StatModifier;
using UnityEngine;
using Utility;
using Utility.Constants;
using Utility.Information;

namespace CharacterModule.Spells.Core
{
    public abstract class SpellBase : ScriptableObject, IObjectInformation, ITargetableObject, ISpell
    {
        [field: SerializeField] 
        public ObjectInformation ObjectInformation { get; set; }
        
        [field: SerializeField]
        public StatModifiers SourceModifiers { get; set; }
        
        [field: SerializeField]
        public TargetType TargetType { get; set; }
        
        [field: SerializeField]
        public TargetSearchType TargetSearchType { get; set; }

        [field: SerializeField]
        [field: Range(BattleTargetingConstants.SpellMin, BattleTargetingConstants.SpellMax)]
        public int MaxTargetsCount { get; set; }

        public abstract void UseSpell(StatManager source, List<Character> targets);
    }
}
