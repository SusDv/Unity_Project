using System.Collections.Generic;
using CharacterModule.Types.Base;

namespace BattleModule.Controllers.Modules.Turn
{
    public class BattleTurnContext
    {
        public List<Character> CharactersInTurn { get; set; }
    }
}