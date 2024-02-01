using System.Collections.Generic;
using StatModule.Interfaces;

namespace CharacterModule.Spells.Interfaces
{
    public interface ISpell
    {
        public void UseSpell(Stats.Base.Stats source, List<Character> targets);
    }
}
