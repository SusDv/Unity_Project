using System.Collections.Generic;
using CharacterModule.Spells.Interfaces;
using CharacterModule.Stats.StatModifier;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
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

        public void UseSpell(Stats.Base.Stats source, List<Character> targets)
        {
            foreach (var target in targets)
            {
                var targetStats = target.GetCharacterStats();
                
                foreach (var baseStatModifier in StatModifiers.BaseModifiers)
                {
                    targetStats.AddStatModifier(baseStatModifier.Clone() as BaseStatModifier);
                }
            }

            source.AddStatModifier(StatType.BATTLE_POINTS, BattlePoints);
        }
    }
}
