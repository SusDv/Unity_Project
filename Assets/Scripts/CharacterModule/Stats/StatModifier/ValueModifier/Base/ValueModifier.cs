using StatModule.Interfaces;
using StatModule.Utility.Enums;

namespace StatModule.Modifier.ValueModifier
{
    public abstract class ValueModifier
    {
        public abstract ValueModifierType ValueModifierType { get; }

        public abstract void ModifyValue(IStat statToModify, float value); 
    }
}