using System.Collections.Generic;
using CharacterModule.CharacterType.Base;
using CharacterModule.Stats.StatModifier;
using UnityEngine;

namespace CharacterModule.Spells.Core.Spells
{
    [CreateAssetMenu(fileName = "Spell", menuName = "Character/Spells/Basic Spell")]
    public class BasicSpell : SpellBase
    {
        [field: SerializeField]
        public StatModifiers TargetModifiers { get; private set; }

        public override void UseSpell(List<Character> targets)
        {
            foreach (var target in targets)
            {
                var targetStats = target.CharacterStats;
                
                foreach (var baseStatModifier in TargetModifiers.GetModifiers())
                {
                    targetStats.StatModifierManager.AddModifier(baseStatModifier);
                }
            }
        }
    }
}
