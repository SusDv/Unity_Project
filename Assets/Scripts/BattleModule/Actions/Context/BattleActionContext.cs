using BattleModule.Actions.Interfaces;
using BattleModule.Utility;
using CharacterModule.Types.Base;
using Utility.Information;

namespace BattleModule.Actions.Context
{
    public class BattleActionContext
    {
        public BattleActionContext(object actionObject, ActionType actionType)
        {
            ObjectInformation = (actionObject as IObjectInformation)?.ObjectInformation;

            BattleObject = actionObject as IBattleObject;

            ActionType = actionType;
        }
        
        public ActionType ActionType { get; }

        public ObjectInformation ObjectInformation { get; }
        
        public IBattleObject BattleObject { get; }
    }
}
