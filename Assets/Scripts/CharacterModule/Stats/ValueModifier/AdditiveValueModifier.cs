using CharacterModule.Stats.ValueModifier.Base;
using CharacterModule.Utility;
using Utility;

namespace CharacterModule.Stats.ValueModifier
{
    public class AdditiveValueModifier : BaseValueModifier
    {
        public override ModifierType ModifierType => ModifierType.ADDITIVE;

        public override void ModifyValue(Ref<float> valueToModify, float value, float modifyFrom)
        {
            valueToModify.Value += value;
        }
    }
}
