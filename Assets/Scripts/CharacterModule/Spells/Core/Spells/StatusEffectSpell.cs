using System.Collections.Generic;
using CharacterModule.Spells.Interfaces;
using StatModule.Interfaces;

namespace CharacterModule.Spells.Core.Spells
{
    public abstract class StatusEffectSpell : SpellBase, ISpell
    {
        public abstract void UseSpell(IHaveStats source, List<Character> targets);
    }
}
