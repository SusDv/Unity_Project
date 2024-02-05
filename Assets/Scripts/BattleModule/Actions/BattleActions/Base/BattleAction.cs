using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Context;
using CharacterModule.Stats.Base;

namespace BattleModule.Actions.BattleActions.Base
{
    public abstract class BattleAction
    {
        protected abstract string ActionName { get; }

        protected BattleActionContext BattleActionContext;
        
        public BattleActionContext GetBattleActionContext() 
        {
            return BattleActionContext;
        }
        
        public void Init(object actionObject)
        {
            BattleActionContext = BattleActionContext
                .GetInstance(ActionName, actionObject);
        }
        
        public abstract void PerformAction(Stats source, List<Character> targets);
    }
}
