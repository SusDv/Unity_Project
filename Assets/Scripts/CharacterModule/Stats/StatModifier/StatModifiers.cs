using System;
using System.Collections.Generic;
using CharacterModule.Stats.Interfaces;

namespace CharacterModule.Stats.StatModifier
{
    [Serializable]
    public abstract class StatModifiers
    {
        protected List<IModifier> Modifiers = new();

        public abstract List<IModifier> GetModifiers();
    }
}
