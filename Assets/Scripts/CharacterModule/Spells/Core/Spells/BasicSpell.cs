using System.Collections.Generic;
using CharacterModule.Spells.Interfaces;
using CharacterModule.Stats.StatModifier;
using StatModule.Interfaces;
using StatModule.Modifier;
using StatModule.Utility.Enums;
using UnityEngine;

namespace CharacterModule.Spells.Core.Spells
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
