using System.Collections.Generic;
using CharacterModule;
using CharacterModule.CharacterType.Base;

namespace BattleModule.Controllers.Modules.Turn
{
    public class BattleTurnContext
    {
        public Character CharacterInAction { get; private set; }

        public List<Character> CharactersInTurn { get; private set; }


        public BattleTurnContext(Character characterInAction, List<Character> charactersInTurn)
        {
            CharacterInAction = characterInAction;
            
            CharactersInTurn = charactersInTurn;
        }
    }
}