using System;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.Modifiers.Base;
using CharacterModule.Utility;

namespace CharacterModule.Stats.Modifiers
{
    [Serializable]
    public class PermanentStatModifier : PermanentModifier<StatType>
    {
        private PermanentStatModifier(
            StatType statType,
            ModifierData modifierData) : base(statType, modifierData)
        { }
        
        public override IModifier<StatType> Clone()
        {
            return new PermanentStatModifier(Type, ModifierData.Clone());
        }
        
        protected override bool Equals(PermanentModifier<StatType> other)
        {
            return Type == other.Type && base.Equals(other);
        }
    }
}
