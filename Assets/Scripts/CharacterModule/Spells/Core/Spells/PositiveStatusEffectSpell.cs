using System.Collections.Generic;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.StatModifier.Modifiers;
using CharacterModule.Stats.Utility.Enums;
using UnityEngine;

namespace CharacterModule.Spells.Core.Spells
{
    [CreateAssetMenu(fileName = "Status Effect Spell", menuName = "Character/Spells/Status Effect Spells/Positive Status Effect Spell")]
    public class PositiveStatusEffectSpell : StatusEffectSpell
    {
        [field: SerializeField]
        private StatModifierTier SpellClearanceTier { get; set; }

        public override void UseSpell(StatManager source, List<Character> targets)
        {
            foreach (var target in targets) 
            {
                target.CharacterStats.RemoveStatModifiersByCondition(
                    (statModifier) => (statModifier is TemporaryStatModifier { IsNegative: true } temporaryStatModifier)
                                      && temporaryStatModifier.StatModifierTier == SpellClearanceTier);
            }

            ApplyCasterModifiers(source);
        }
        
        private void ApplyCasterModifiers(StatManager source)
        {
            foreach (var modifier in CasterModifiers.BaseModifiers)
            {
                source.ApplyStatModifier(modifier);
            }
        }
    }
}
