using BattleModule.Utility.Interfaces;
using Utility;
using Utility.Information;

namespace BattleModule.Actions.BattleActions.Context
{
    public class BattleActionContext
    {
        public BattleActionContext(object actionObject)
        {
            ObjectInformation = (actionObject as IObjectInformation)?.ObjectInformation;

            ActionObject = actionObject;

            TargetableObjectObject = actionObject as ITargetableObject;
        }
        public ObjectInformation ObjectInformation { get; }

        public object ActionObject { get; }

        public ITargetableObject TargetableObjectObject { get; }
    }
}
