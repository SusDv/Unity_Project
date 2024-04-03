using System.Collections.Generic;
using BattleModule.Utility;
using BattleModule.Utility.Interfaces;
using CharacterModule.Spells.Interfaces;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.StatModifier;
using UnityEngine;
using Utility.Constants;

namespace CharacterModule.Spells.Core
{
    public abstract class SpellBase : ScriptableObject, ITargeting, ISpell
    {
        [field: SerializeField]
        public string SpellName { get; private set; }

        [field: SerializeField]
        public Sprite SpellImage { get; private set; }

        [field: SerializeField]
        [field: TextArea]
        public string SpellDescription { get; private set; }

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
