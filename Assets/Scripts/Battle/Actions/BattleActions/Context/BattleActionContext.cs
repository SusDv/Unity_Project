using BattleModule.Utility.Enums;
using BattleModule.Utility.Interfaces;

namespace BattleModule.ActionCore.Context
{
    public class BattleActionContext
    {
        private BattleActionContext(string actionName,
            object actionObject)
        {
            ActionName = actionName;

            ActionObject = actionObject;

            ITargetable targetable = actionObject as ITargetable;

            TargetType = targetable.TargetType;

            TargetSearchType = targetable.TargetSearchType;

            MaxTargetCount = targetable.MaxTargetsCount;
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
