using CharacterModule.Stats.Base;
using StatModule.Utility.Enums;

namespace StatModule.Modifier.ValueModifier
{
    public abstract class ValueModifier
    {
        public abstract ValueModifierType ValueModifierType { get; }

        public abstract void ModifyValue(Stat statToModify, float value, ModifierCapType modifierCapType); 
    }
}