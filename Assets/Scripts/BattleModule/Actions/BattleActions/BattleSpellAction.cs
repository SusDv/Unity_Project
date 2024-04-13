using System;
using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Base;
using CharacterModule.CharacterType.Base;
using CharacterModule.Spells.Interfaces;

namespace BattleModule.Actions.BattleActions
{
    public class BattleSpellAction : BattleAction
    {
        public override void PerformAction(Character source, 
            List<Character> targets, 
            Action actionFinishedCallback)
        {
            (BattleActionContext.ActionObject as ISpell)?.UseSpell(targets);

            base.PerformAction(source, targets, actionFinishedCallback);
        }
    }
}
