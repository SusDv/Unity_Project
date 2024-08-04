using System;
using BattleModule.Actions.Interfaces;
using BattleModule.Utility;
using Utility.Information;

namespace BattleModule.Actions.Context
{
    public class BattleActionContext
    {
        public BattleActionContext(object actionObject, 
            Type characterInActionType, ActionType actionType)
        {
            ObjectInformation = (actionObject as IObjectInformation)?.ObjectInformation;

            BattleObject = actionObject as IBattleObject;

            CharacterInActionType = characterInActionType;
            
            ActionType = actionType;
        }

        public Type CharacterInActionType { get; }

        public ActionType ActionType { get; }

        public ObjectInformation ObjectInformation { get; }
        
        public IBattleObject BattleObject { get; }
    }
}
