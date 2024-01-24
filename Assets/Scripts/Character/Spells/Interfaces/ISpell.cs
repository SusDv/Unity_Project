using StatModule.Interfaces;
using System.Collections.Generic;

namespace SpellModule.Interfaces
{
    public interface ISpell
    {
        public void UseSpell(IHaveStats source, List<Character> targets);
    }
}
