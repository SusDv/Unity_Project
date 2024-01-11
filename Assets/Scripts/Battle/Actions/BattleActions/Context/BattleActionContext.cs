using BattleModule.Utility.Enums;

namespace BattleModule.ActionCore.Context
{
    public class BattleActionContext
    {
        private BattleActionContext(
            object actionObject, TargetType targetType)
        {
            ActionObject = actionObject;
            TargetType = targetType;
        }

        public object ActionObject { get; set; }

        public TargetType TargetType { get; set; }   

        public static BattleActionContext GetBattleActionContextInstance(
            object actionObject, TargetType targetType)
        {
            return new BattleActionContext(actionObject, targetType);
        }
    }
}
