using BattleModule.ActionCore.Context;
using System;

namespace BattleModule.ActionCore.Interfaces
{
    public interface IBattleAction
    {
        public event Action<BattleActionContext> OnBattleActionChanged;

        public void SetBattleAction<T>(object actionObject)
            where T : BattleAction;

        public void ResetBattleAction();
    }
}
