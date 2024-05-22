using System.Collections.Generic;
using CharacterModule.Types.Base;

namespace BattleModule.Utility.Interfaces
{
    public interface ICharacterInTurnObserver
    {
        public void Notify(List<Character> characters);
    }
}