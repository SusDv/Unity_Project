using BattleModule.Actions.BattleActions.Interfaces;
using BattleModule.Utility.Interfaces;
using Utility;
using Utility.Information;

namespace BattleModule.Actions.BattleActions.Context
{
    public class BattleActionContext
    {
        public BattleActionContext(object actionObject)
        {
            ObjectInformation = actionObject as IObjectInformation;

            BattleObject = actionObject as IBattleObject;

            ActionObject = actionObject;
        }
        
        public IObjectInformation ObjectInformation { get; }
        
        public IBattleObject BattleObject { get; }

        public object ActionObject { get; }
    }
}
