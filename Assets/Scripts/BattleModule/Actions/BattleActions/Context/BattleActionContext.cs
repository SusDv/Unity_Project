using BattleModule.Utility.Enums;
using BattleModule.Utility.Interfaces;

namespace BattleModule.Actions.BattleActions.Context
{
    public class BattleActionContext
    {
        private readonly ITargeting _targetingObject; 
        
        private BattleActionContext(string actionName,
            object actionObject)
        {
            ActionName = actionName;

            ActionObject = actionObject;

            _targetingObject = actionObject as ITargeting;
        }
        public string ActionName { get; }

        public object ActionObject { get; }

        public TargetType TargetType => _targetingObject.TargetType;

        public TargetSearchType TargetSearchType => _targetingObject.TargetSearchType;

        public int MaxTargetCount => _targetingObject.MaxTargetsCount;

        public static BattleActionContext GetInstance(string actionName,
            object actionObject)
        {
            return new BattleActionContext(actionName, actionObject);
        }
    }
}
