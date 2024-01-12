using BattleModule.ActionCore.Context;

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
