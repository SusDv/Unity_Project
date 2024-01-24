using SpellModule.Interfaces;
using StatModule.Interfaces;
using StatModule.Modifier;
using StatModule.Utility.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace SpellModule.Base
{
    [CreateAssetMenu(fileName = "Spell", menuName = "Character/Spells/Spell")]
    public class BasicSpell : Spell, ISpell
    {
        public void UseSpell(IHaveStats source, List<Character> targets)
        {
            foreach (Character target in targets)
            {
                foreach (BaseStatModifier baseStatModifier in StatModifiers.BaseModifiers)
                {
                    IHaveStats targetStats = target.GetCharacterStats();

                    targetStats.AddStatModifier(baseStatModifier);
                }
            }
            source.AddStatModifier(StatType.BATTLE_POINTS, BattlePoints);
        }
    }
}
