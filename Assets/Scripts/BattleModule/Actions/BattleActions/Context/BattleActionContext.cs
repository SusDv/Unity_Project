using System;
using BattleModule.Actions.BattleActions.Interfaces;
using CharacterModule.Types.Base;
using Utility.Information;

namespace BattleModule.Actions.BattleActions.Context
{
    public class BattleActionContext
    {
        public BattleActionContext(object actionObject, 
            Type currentCharacterType)
        {
            CharacterInTurnType = currentCharacterType;
            
            ObjectInformation = (actionObject as IObjectInformation)?.ObjectInformation;

            BattleObject = actionObject as IBattleObject;
        }

        public Type CharacterInTurnType { get; }

        public ObjectInformation ObjectInformation { get; }
        
        public IBattleObject BattleObject { get; }
    }
}
