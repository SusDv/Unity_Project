using System.Collections.Generic;
using CharacterModule.CharacterType.Base;

namespace BattleModule.Controllers.Modules.Turn
{
    public class BattleTurnContext
    {
        public Character CharacterInAction { get; set; }

        public List<Character> CharactersInTurn { get; set; }
    }
}