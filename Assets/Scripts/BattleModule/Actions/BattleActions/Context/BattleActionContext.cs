using BattleModule.Actions.BattleActions.Interfaces;
using CharacterModule.Types.Base;
using Utility.Information;

namespace BattleModule.Actions.BattleActions.Context
{
    public class BattleActionContext
    {
        public BattleActionContext(object actionObject, Character currentCharacterType)
        {
            CurrentCharacter = currentCharacterType;
            
            ObjectInformation = actionObject as IObjectInformation;

            BattleObject = actionObject as IBattleObject;
        }

        public Character CurrentCharacter { get; }

        public IObjectInformation ObjectInformation { get; }
        
        public IBattleObject BattleObject { get; }
    }
}
