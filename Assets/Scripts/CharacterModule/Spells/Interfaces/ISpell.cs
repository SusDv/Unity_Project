using System.Collections.Generic;
using CharacterModule.CharacterType.Base;

namespace CharacterModule.Spells.Interfaces
{
    public interface ISpell
    {
        public void UseSpell(List<Character> targets);
    }
}
