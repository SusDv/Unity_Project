using StatModule.Interfaces;
using StatModule.Utility.Enums;

namespace StatModule.Modifier.ValueModifier
{
    public class AdditiveValueModifier : ValueModifier
    {
        public override ValueModifierType ValueModifierType => ValueModifierType.ADDITIVE;

        public override void ModifyValue(IStat statToModify, float value)
        {
            statToModify.FinalValue += value;
        }
    }
}
