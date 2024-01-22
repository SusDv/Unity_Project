using BattleModule.ActionCore.Context;
using StatModule.Interfaces;
using System;
using System.Collections.Generic;

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

        public abstract void PerformAction(IHaveStats source, List<Character> targets);
    }
}
