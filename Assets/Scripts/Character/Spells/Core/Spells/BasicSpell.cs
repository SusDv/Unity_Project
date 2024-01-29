using SpellModule.Interfaces;
using StatModule.Interfaces;
using StatModule.Modifier;
using StatModule.Utility.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace SpellModule.Base
{
    [CreateAssetMenu(fileName = "Spell", menuName = "Character/Spells/Basic Spell")]
    public class BasicSpell : SpellBase, ISpell
    {
        [field: SerializeField]
        public StatModifiers StatModifiers { get; set; }

        public void UseSpell(IHaveStats source, List<Character> targets)
        {
            foreach (Character target in targets)
            {
                foreach (BaseStatModifier baseStatModifier in StatModifiers.BaseModifiers)
                {
                    IHaveStats targetStats = target.GetCharacterStats();

                    targetStats.AddStatModifier(baseStatModifier.Clone() as BaseStatModifier);
                }
            }

            source.AddStatModifier(StatType.BATTLE_POINTS, BattlePoints);
        }
    }
}
