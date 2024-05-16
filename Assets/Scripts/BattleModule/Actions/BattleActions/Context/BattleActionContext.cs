using BattleModule.Actions.BattleActions.Interfaces;
using Utility.Information;

namespace BattleModule.Actions.BattleActions.Context
{
    public class BattleActionContext
    {
        public BattleActionContext(object actionObject)
        {
            ObjectInformation = actionObject as IObjectInformation;

            BattleObject = actionObject as IBattleObject;
        }
        
        public IObjectInformation ObjectInformation { get; }
        
        public IBattleObject BattleObject { get; }
    }
}
