using System.Collections.Generic;
using CharacterModule.Stats.Managers;

namespace CharacterModule.Spells.Interfaces
{
    public interface ISpell
    {
        public void UseSpell(StatManager source, List<Character> targets);
    }
}
