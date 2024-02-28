using System.Collections.Generic;
using CharacterModule;

namespace BattleModule.Utility.Interfaces
{
    public interface ICharacterInTurnObserver
    {
        public void Notify(List<Character> characters);
    }
}