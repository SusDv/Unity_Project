using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Base;
using CharacterModule.Spells.Interfaces;
using StatModule.Interfaces;

namespace BattleModule.Actions.BattleActions
{
    public class BattleSpellAction : BattleAction
    {
        protected override string ActionName => "Spell use";

        public BattleSpellAction()
            : base()
        { }

        private BattleSpellAction(object actionObject) 
            : base(actionObject) 
        {}       

        public override void PerformAction(IHaveStats source, List<Character> targets)
        {
            (BattleActionContext.ActionObject as ISpell).UseSpell(source, targets);
        }

        public override BattleAction GetInstance(object actionObject)
        {
            return new BattleSpellAction(actionObject);
        }
    }
}
