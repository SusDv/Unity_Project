using System;
using System.Collections.Generic;
using CharacterModule.Stats.StatModifier.Modifiers;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
using StatModule.Modifier;
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

        public List<BaseStatModifier> BaseModifiers 
        {
            get 
            {
                List<BaseStatModifier> allModifiers = new List<BaseStatModifier>();

                allModifiers.AddRange(_instantModifiers);

                allModifiers.AddRange(_temporaryModifiers);

                allModifiers.AddRange(_permanentModifiers);

                return allModifiers;
            }
        }
    }
}
