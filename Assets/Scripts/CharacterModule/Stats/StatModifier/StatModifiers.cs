using System;
using System.Collections.Generic;
using System.Linq;
using CharacterModule.Stats.Interfaces;

namespace CharacterModule.Stats.StatModifier
{
    [Serializable]
    public abstract class StatModifiers
    {
        protected List<IModifier> Modifiers = new();

        protected List<ITemporaryModifier> TemporaryModifiers = new();

        public virtual (List<ITemporaryModifier> temporaryModifiers, List<IModifier> modifiers) GetModifiers()
        {
            return (TemporaryModifiers.Select(t => t.Clone()).Cast<ITemporaryModifier>().ToList(), Modifiers.Select(m => m.Clone()).ToList());
        }

        public void SetSourceID(int id)
        {
            var modifiers = new List<IModifier>();
            
            modifiers.AddRange(Modifiers);
            
            modifiers.AddRange(TemporaryModifiers);
            
            modifiers.ForEach(m => m.ModifierData.SourceID = id);
        }

        protected void ClearLists()
        {
            Modifiers.Clear();
            
            TemporaryModifiers.Clear();
        }
    }
}
