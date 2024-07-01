using System;
using System.Collections.Generic;
using System.Linq;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Utility;

namespace CharacterModule.Stats.Modifiers.Containers
{
    [Serializable]
    public abstract class StatModifiers
    {
        protected List<IModifier<StatType>> Modifiers = new();

        protected List<ITemporaryModifier<StatType>> TemporaryModifiers = new();

        public virtual (List<ITemporaryModifier<StatType>> temporaryModifiers, List<IModifier<StatType>> modifiers) GetModifiers()
        {
            return (TemporaryModifiers.Select(t => t.Clone()).Cast<ITemporaryModifier<StatType>>().ToList(), Modifiers.Select(m => m.Clone()).ToList());
        }

        public void SetSourceID(int id)
        {
            var modifiers = new List<IModifier<StatType>>();
            
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
