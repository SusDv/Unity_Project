using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Context;
using CharacterModule;
using CharacterModule.Stats.Managers;

namespace BattleModule.Actions.BattleActions.Base
{
    public abstract class BattleAction
    {
        protected BattleActionContext BattleActionContext;
        public BattleActionContext GetBattleActionContext() 
        {
            return BattleActionContext;
        }

        public abstract void Init(object actionObject);

        public abstract void PerformAction(StatManager source, List<Character> targets);
    }
}
