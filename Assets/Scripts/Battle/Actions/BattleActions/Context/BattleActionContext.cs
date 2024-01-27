using BattleModule.Utility.Enums;
using BattleModule.Utility.Interfaces;

namespace BattleModule.ActionCore.Context
{
    public class BattleActionContext
    {
        private BattleActionContext(
            object actionObject)
        {
            ActionObject = actionObject;

            ITargetable targetable = actionObject as ITargetable;

            TargetType = targetable.TargetType;
            TargetSearchType = targetable.TargetSearchType;
            MaxTargetsCount = targetable.MaxTargetsCount;
        }

        public object ActionObject { get; }

        public TargetType TargetType { get; }

        public TargetSearchType TargetSearchType { get; }

        public int MaxTargetsCount { get; }

        public static BattleActionContext GetInstance(
            object actionObject)
        {
            return new BattleActionContext(actionObject);
        }
    }
}
