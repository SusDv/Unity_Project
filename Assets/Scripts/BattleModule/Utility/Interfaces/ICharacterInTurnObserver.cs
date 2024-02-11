using System.Collections.Generic;

namespace BattleModule.Utility.Interfaces
{
    public interface ICharacterInTurnObserver
    {
        public void Notify(List<Character> characters);
    }
}