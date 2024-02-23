using System.Collections.Generic;

namespace CharacterModule.Spells.Interfaces
{
    public interface ISpell
    {
        public void UseSpell(Stats.Base.Stats source, List<Character> targets);
    }
}
