using CharacterModule.Stats.StatModifier.ValueModifier.Base;
using CharacterModule.Utility;
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
