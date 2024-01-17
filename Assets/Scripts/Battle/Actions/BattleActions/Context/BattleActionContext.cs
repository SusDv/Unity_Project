using BattleModule.Utility.Enums;
using BattleModule.Utility.Interfaces;

namespace BattleModule.ActionCore.Context
{
    public class BattleActionContext
    {
        private BattleActionContext(
            object actionObject, 
            ITargetable targetableObject)
        {
            ActionObject = actionObject;

            TargetType = targetableObject.TargetType;
            TargetSearchType = targetableObject.TargetSearchType;
            TargetsToSelect = targetableObject.TargetsToSelect;
            MaxTargetsCount = targetableObject.MaxTargetsCount;
        }

        public object ActionObject { get; set; }

        public TargetType TargetType { get; set; }

        public TargetSearchType TargetSearchType { get; set; }

        public int TargetsToSelect { get; set; }

        public int MaxTargetsCount { get; set; }

        public static BattleActionContext GetBattleActionContextInstance(
            object actionObject, ITargetable targetableObject)
        {
            return new BattleActionContext(actionObject, targetableObject);
        }
    }
}
