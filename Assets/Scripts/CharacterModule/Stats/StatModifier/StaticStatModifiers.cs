using System;
using System.Collections.Generic;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.StatModifier.Modifiers;
using UnityEngine;

namespace CharacterModule.Stats.StatModifier
{
    [Serializable]
    public class StaticStatModifiers : StatModifiers
    {
        [field: SerializeField] 
        private List<PermanentStatModifier> _permanentStatModifiers = new();
        
        public override List<IModifier> GetModifiers()
        {
            Modifiers = new List<IModifier>();
            
            Modifiers.AddRange(_permanentStatModifiers);

            return Modifiers;
        }
    }
}