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

        public object ActionObject { get; set; }

        public TargetType TargetType { get; set; }

        public TargetSearchType TargetSearchType { get; set; }

        public int MaxTargetsCount { get; set; }

        public static BattleActionContext GetBattleActionContextInstance(
            object actionObject)
        {
            return new BattleActionContext(actionObject);
        }
    }
}
