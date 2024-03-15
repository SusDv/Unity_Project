using CharacterModule.Stats.StatModifier.ValueModifier.Base;
using StatModule.Utility.Enums;
using Utility;

namespace CharacterModule.Stats.StatModifier.ValueModifier
{
    public class AdditiveValueModifier : BaseValueModifier
    {
        public override ValueModifierType ValueModifierType => ValueModifierType.ADDITIVE;

        public override void ModifyValue(Ref<float> valueToModify, float value)
        {
            valueToModify.Value += value;
        }
    }
}
