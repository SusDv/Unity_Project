using BattleModule.Utility.Enums;
using BattleModule.Utility.Interfaces;
using StatModule.Modifier;
using System;
using UnityEngine;

namespace SpellModule.Base
{
    public class SpellBase : ScriptableObject, ITargetable 
    {
        [field: SerializeField]
        public string SpellName { get; set; }

        [field: SerializeField]
        public Sprite SpellImage { get; set; }

        [field: SerializeField]
        [field: TextArea]
        public string SpellDescription { get; set; }

        [field: SerializeField]
        public float BattlePoints { get; set; }

        [field: SerializeField]
        public TargetType TargetType { get; set; }
        
        [field: SerializeField]
        public TargetSearchType TargetSearchType { get; set; }

        [field: SerializeField]
        [field: Range(1, 5)]
        public int MaxTargetsCount { get; set; } = 1;      
    }
}
