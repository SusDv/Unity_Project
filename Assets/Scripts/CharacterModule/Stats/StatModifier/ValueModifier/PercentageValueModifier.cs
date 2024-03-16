using CharacterModule.Stats.StatModifier.ValueModifier.Base;
using CharacterModule.Stats.Utility.Enums;
using Utility;

namespace CharacterModule.Stats.StatModifier.ValueModifier
{
    public class PercentageValueModifier : BaseValueModifier
    {
        public override ValueModifierType ValueModifierType => ValueModifierType.PERCENTAGE;

        public override void ModifyValue(Ref<float> valueToModify, float value)
        {
            valueToModify.Value *= value;
        }
    }
}
