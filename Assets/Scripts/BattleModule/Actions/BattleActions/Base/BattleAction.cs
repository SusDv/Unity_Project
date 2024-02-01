using StatModule.Interfaces;
using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Context;
using CharacterModule.Stats.Base;

namespace BattleModule.Actions.BattleActions.Base
{
    public abstract class BattleAction
    {
        protected abstract string ActionName { get; }

        protected readonly BattleActionContext BattleActionContext;

        protected BattleAction() { }

        protected BattleAction(object actionObject)
        {
            BattleActionContext = BattleActionContext
                .GetInstance(ActionName, actionObject);
        }

        public BattleActionContext GetBattleActionContext() 
        {
            return BattleActionContext;
        }

        public abstract void PerformAction(Stats source, List<Character> targets);

        public abstract BattleAction GetInstance(object actionObject);
    }
}
