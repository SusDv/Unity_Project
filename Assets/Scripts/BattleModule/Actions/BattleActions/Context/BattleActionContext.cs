using BattleModule.Utility.Interfaces;

namespace BattleModule.Actions.BattleActions.Context
{
    public class BattleActionContext
    {
        public BattleActionContext(string actionName,
            object actionObject)
        {
            ActionName = actionName;

            ActionObject = actionObject;

            TargetingObject = actionObject as ITargeting;
        }
        public string ActionName { get; }

        public object ActionObject { get; }

        public ITargeting TargetingObject { get; }
    }
}
