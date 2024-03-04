using CharacterModule.Stats.Base;
using CharacterModule.Stats.Interfaces;
using StatModule.Utility.Enums;

namespace StatModule.Modifier.ValueModifier
{
    public class AdditiveValueModifier : ValueModifier
    {
        public override ValueModifierType ValueModifierType => ValueModifierType.ADDITIVE;

        public override void ModifyValue(Stat statToModify, float value, ModifierCapType modifierCapType)
        {
            if (modifierCapType == ModifierCapType.FINAL_VALUE)
            {
                statToModify.FinalValue += value;
                
                return;
            }

            statToModify.MaxValue += value;
        }
    }
}
