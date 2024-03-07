using System.Collections.Generic;
using CharacterModule.Spells.Interfaces;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.StatModifier;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
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

        public void UseSpell(StatManager source, List<Character> targets)
        {
            foreach (var target in targets)
            {
                var targetStats = target.CharacterStats;
                
                foreach (var baseStatModifier in StatModifiers.BaseModifiers)
                {
                    targetStats.ApplyStatModifier(baseStatModifier.Clone() as BaseStatModifier);
                }
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
