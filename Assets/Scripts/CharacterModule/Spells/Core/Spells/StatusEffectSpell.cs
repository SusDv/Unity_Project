using System.Collections.Generic;
using CharacterModule.Spells.Interfaces;
using CharacterModule.Stats.Managers;

namespace CharacterModule.Spells.Core.Spells
{
    public abstract class StatusEffectSpell : SpellBase, ISpell
    {
        public abstract void UseSpell(StatManager source, List<Character> targets);
    }
}
