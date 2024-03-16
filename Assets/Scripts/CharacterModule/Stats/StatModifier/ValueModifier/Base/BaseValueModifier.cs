using CharacterModule.Stats.Utility.Enums;
using Utility;

namespace CharacterModule.Stats.StatModifier.ValueModifier.Base
{
    public abstract class BaseValueModifier
    {
        public abstract ValueModifierType ValueModifierType { get; }

        public abstract void ModifyValue(Ref<float> valueToModify, float value); 
    }
}