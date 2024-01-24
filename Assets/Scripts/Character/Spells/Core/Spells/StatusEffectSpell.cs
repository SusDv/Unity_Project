using SpellModule.Interfaces;
using StatModule.Interfaces;
using System.Collections.Generic;

namespace SpellModule.Base
{
    public abstract class StatusEffectSpell : SpellBase, ISpell
    {
        public abstract void UseSpell(IHaveStats source, List<Character> targets);
    }
}
