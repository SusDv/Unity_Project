using System;
using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Base;
using CharacterModule.CharacterType.Base;

namespace BattleModule.Actions.BattleActions
{
    public class SpecialAction : BattleAction
    {
        public override void PerformAction(Character source, List<Character> targets, Action actionFinishedCallback)
        {
            source.CharacterWeapon.GetSpecialAttack().Attack(targets);
            
            base.PerformAction(source, targets, actionFinishedCallback);
        }
    }
}