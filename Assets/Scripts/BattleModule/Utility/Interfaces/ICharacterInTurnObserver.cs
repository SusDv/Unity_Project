using System.Collections.Generic;
using CharacterModule;
using CharacterModule.CharacterType.Base;

namespace BattleModule.Utility.Interfaces
{
    public interface ICharacterInTurnObserver
    {
        public void Notify(List<Character> characters);
    }
}