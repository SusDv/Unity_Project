using StatModule.Interfaces;
using StatModule.Modifier;
using StatModule.Utility.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace SpellModule.Base
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
                    (statModifier) => (statModifier is TemporaryStatModifier temporaryStatModifier) 
                    && temporaryStatModifier.IsNegative
                    && temporaryStatModifier.TemporaryStatModifierTier == SpellClearanceTier);
            }

            source.AddStatModifier(StatType.BATTLE_POINTS, BattlePoints);
        }
    }
}
