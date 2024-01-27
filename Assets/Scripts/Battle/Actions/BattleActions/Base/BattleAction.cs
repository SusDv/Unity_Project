using BattleModule.ActionCore.Context;
using StatModule.Interfaces;
using System.Collections.Generic;

namespace BattleModule.ActionCore
{
    public abstract class BattleAction
    {
        public abstract string ActionName { get; }

        protected BattleActionContext _battleActionContext;

        protected BattleAction() { }

        protected BattleAction(object actionObject)
        {
            _battleActionContext = BattleActionContext
                .GetInstance(ActionName, actionObject);
        }

        public BattleActionContext GetBattleActionContext() 
        {
            return _battleActionContext;
        }

        public abstract void PerformAction(IHaveStats source, List<Character> targets);

        public abstract BattleAction GetInstance(object actionObject);
    }
}
