using System;
using CharacterModule.Stats.ValueModifier.Base;
using CharacterModule.Utility;
using Utility;

namespace CharacterModule.Stats.ValueModifier
{
    public class PercentageValueModifier : BaseValueModifier
    {
        public override ModifierType ModifierType => ModifierType.PERCENTAGE;

        public override void ModifyValue(Ref<float> valueToModify, float value, float modifyFrom)
        {
            valueToModify.Value += (float) Math.Round(value / 100f, 2) * modifyFrom;
        }
    }
}
