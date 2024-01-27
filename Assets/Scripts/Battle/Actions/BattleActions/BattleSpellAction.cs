using System.Collections.Generic;
using SpellModule.Interfaces;
using StatModule.Interfaces;

namespace BattleModule.ActionCore
{
    public class BattleSpellAction : BattleAction
    {
        public override string ActionName => "Spell use";

        public BattleSpellAction()
            : base()
        { }

        public BattleSpellAction(object actionObject) 
            : base(actionObject) 
        {}       

        public override void PerformAction(IHaveStats source, List<Character> targets)
        {
            (_battleActionContext.ActionObject as ISpell).UseSpell(source, targets);
        }

        public override BattleAction GetInstance(object actionObject)
        {
            return new BattleSpellAction(actionObject);
        }
    }
}
