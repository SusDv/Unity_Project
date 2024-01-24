using System.Collections.Generic;
using BattleModule.ActionCore.Context;
using SpellModule.Interfaces;
using StatModule.Interfaces;

namespace BattleModule.ActionCore
{
    public class BattleSpellAction : BattleAction
    {
        public BattleSpellAction(BattleActionContext battleActionContext) 
            : base(battleActionContext) 
        {}

        public override string ActionName => "Spell use";

        public override void PerformAction(IHaveStats source, List<Character> targets)
        {
            (_battleActionContext.ActionObject as ISpell).UseSpell(source, targets);
        }

        public static BattleSpellAction GetBattleSpellActionInstance(BattleActionContext battleActionContext) 
        {
            return new BattleSpellAction(battleActionContext);
        }
    }
}
