using System.Collections.Generic;
using CharacterModule.Spells.Interfaces;

namespace CharacterModule.Spells.Core.Spells
{
    public abstract class StatusEffectSpell : SpellBase, ISpell
    {
        public abstract void UseSpell(Stats.Base.StatManager source, List<Character> targets);
    }
}
