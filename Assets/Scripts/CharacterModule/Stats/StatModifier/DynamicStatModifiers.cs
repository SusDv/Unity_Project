using System;
using System.Collections.Generic;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.Modifiers;
using CharacterModule.Utility;
using UnityEngine;

namespace CharacterModule.Stats.StatModifier
{
    [Serializable]
    public class DynamicStatModifiers : StatModifiers
    {
        [field: SerializeField] 
        private List<InstantStatModifier> _instantStatModifiers = new();
        
        [field: SerializeField] 
        private List<TemporaryStatModifier> _temporaryStatModifiers = new();
        
        public override (List<ITemporaryModifier<StatType>>, List<IModifier<StatType>>) GetModifiers()
        {
            ClearLists();
            
            Modifiers.AddRange(_instantStatModifiers);
            
            TemporaryModifiers.AddRange(_temporaryStatModifiers);
            
            return base.GetModifiers();
        }
    }
}