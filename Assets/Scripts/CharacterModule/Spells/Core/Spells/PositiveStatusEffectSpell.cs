using System.Collections.Generic;
using CharacterModule.Stats.StatModifier.Modifiers;
using StatModule.Interfaces;
using StatModule.Modifier;
using StatModule.Utility.Enums;
using UnityEngine;

namespace CharacterModule.Spells.Core.Spells
{
    [CreateAssetMenu(fileName = "Status Effect Spell", menuName = "Character/Spells/Status Effect Spells/Positive Status Effect Spell")]
    public class PositiveStatusEffectSpell : StatusEffectSpell
    {
        [field: SerializeField]
        private StatModifierTier SpellClearanceTier { get; set; }

        public override void UseSpell(IHaveStats source, List<Character> targets)
        {
            foreach (Character target in targets) 
            {
                target.GetCharacterStats().RemoveStatModifiersByCondifition(
                    (statModifier) => (statModifier is TemporaryStatModifier { IsNegative: true } temporaryStatModifier)
                                      && temporaryStatModifier.TemporaryStatModifierTier == SpellClearanceTier);
            }

            source.AddStatModifier(StatType.BATTLE_POINTS, BattlePoints);
        }
    }
}
