using System;
using System.Collections.Generic;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.StatModifier.Modifiers;
using UnityEngine;

namespace CharacterModule.Stats.StatModifier
{
    [Serializable]
    public class StatModifiers
    {
        [field: SerializeField]
        private List<InstantStatModifier> _instantModifiers = new();

        [field: SerializeField]
        private List<TemporaryStatModifier> _temporaryModifiers = new();

        [field: SerializeField]
        private List<PermanentStatModifier> _permanentModifiers = new();
        
        public List<IModifier> GetModifiers()
        {
            var allModifiers = new List<IModifier>();

            allModifiers.AddRange(_instantModifiers);

            allModifiers.AddRange(_temporaryModifiers);

            allModifiers.AddRange(_permanentModifiers);

            return allModifiers;
        }
    }
}
