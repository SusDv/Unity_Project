using System.Collections.Generic;
using StatModule.Interfaces;

namespace CharacterModule.Spells.Interfaces
{
    public interface ISpell
    {
        public void UseSpell(IHaveStats source, List<Character> targets);
    }
}
