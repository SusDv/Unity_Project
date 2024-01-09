using System;
using System.Collections.Generic;
using UnityEngine;

namespace StatModule.Modifier
{
    [Serializable]
    public class StatModifiers
    {
        [field: SerializeField]
        private List<InstantStatModifier> _instantModifiers = new List<InstantStatModifier>();

        [field: SerializeField]
        private List<TemporaryStatModifier> _temporaryModifiers = new List<TemporaryStatModifier>();

        [field: SerializeField]
        private List<PermanentStatModifier> _permanentModifiers = new List<PermanentStatModifier>();

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
