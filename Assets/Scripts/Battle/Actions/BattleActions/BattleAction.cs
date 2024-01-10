using BattleModule.Utility.Enums;

namespace BattleModule.ActionCore
{
    public abstract class BattleAction
    {
        protected object _actionObject;

        protected TargetType _targetType;

        protected BattleAction(object actionObject, TargetType targetType)
        {
            _actionObject = actionObject;
            _targetType = targetType;
        }

        public TargetType GetTargetType() 
        {
            return _targetType;
        }

        public abstract void PerformAction(Character source, Character target);
    }
}
