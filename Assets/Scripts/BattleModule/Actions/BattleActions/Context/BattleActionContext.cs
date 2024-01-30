using BattleModule.Utility.Enums;
using BattleModule.Utility.Interfaces;

namespace BattleModule.Actions.BattleActions.Context
{
    public class BattleActionContext
    {
        private BattleActionContext(string actionName,
            object actionObject)
        {
            ActionName = actionName;

            ActionObject = actionObject;

            ITargeting targeting = actionObject as ITargeting;

            TargetType = targeting.TargetType;

            TargetSearchType = targeting.TargetSearchType;

            MaxTargetCount = targeting.MaxTargetsCount;
        }
        public string ActionName { get; }

        public object ActionObject { get; }

        public TargetType TargetType { get; }

        public TargetSearchType TargetSearchType { get; }

        public int MaxTargetCount { get; }

        public static BattleActionContext GetInstance(string actionName,
            object actionObject)
        {
            return new BattleActionContext(actionName, actionObject);
        }
    }
}
