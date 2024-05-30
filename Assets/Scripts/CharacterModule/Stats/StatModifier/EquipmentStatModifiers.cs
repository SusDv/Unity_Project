using System;
using System.Collections.Generic;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.Modifiers;
using CharacterModule.Utility;
using UnityEngine;

namespace CharacterModule.Stats.StatModifier
{
    [Serializable]
    public class EquipmentStatModifiers : StatModifiers
    {
        [field: SerializeField] 
        private List<PermanentStatModifier> _permanentStatModifiers = new();
        
        public override (List<ITemporaryModifier<StatType>>, List<IModifier<StatType>>) GetModifiers()
        {
            ClearLists();
            
            Modifiers.AddRange(_permanentStatModifiers);

            return base.GetModifiers();
        }
    }
}