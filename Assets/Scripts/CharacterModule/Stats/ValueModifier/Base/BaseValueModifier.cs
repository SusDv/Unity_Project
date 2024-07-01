using CharacterModule.Utility;
using Utility;

namespace CharacterModule.Stats.ValueModifier.Base
{
    public abstract class BaseValueModifier
    {
        public abstract ModifierType ModifierType { get; }

        public abstract void ModifyValue(Ref<float> valueToModify, float value, float modifyFrom); 
    }
}