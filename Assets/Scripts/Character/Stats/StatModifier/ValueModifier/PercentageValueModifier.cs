using StatModule.Interfaces;
using StatModule.Utility.Enums;

namespace StatModule.Modifier.ValueModifier
{
    public class PercentageValueModifier : ValueModifier
    {
        public override ValueModifierType ValueModifierType => ValueModifierType.PERCENTAGE;

        public override void ModifyValue(IStat statToModify, float value)
        {
            statToModify.FinalValue += statToModify.BaseValue * value;
        }
    }
}
