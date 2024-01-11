using BattleModule.ActionCore.Context;
using BattleModule.Utility.Enums;

namespace BattleModule.ActionCore
{
    public abstract class BattleAction
    {
        protected BattleActionContext _battleActionContext;

        protected BattleAction(BattleActionContext battleActionContext)
        {
            _battleActionContext = battleActionContext;
        }

        public BattleActionContext GetBattleActionContext() 
        {
            return _battleActionContext;
        }

        public abstract void PerformAction(Character source, Character target);
    }
}
