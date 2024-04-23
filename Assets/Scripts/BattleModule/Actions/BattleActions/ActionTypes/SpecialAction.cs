using System;
using System.Collections.Generic;
using BattleModule.AccuracyModule;
using BattleModule.Actions.BattleActions.Base;
using CharacterModule.CharacterType.Base;

namespace BattleModule.Actions.BattleActions.ActionTypes
{
    public class SpecialAction : BattleAction
    {
        private SpecialAction()
        {
            Accuracy = new DamageAccuracy();
        }

        public override void PerformAction(Character source, List<Character> targets, Action actionFinishedCallback)
        {
            source.CharacterWeapon.GetSpecialAttack().Attack(targets);
            
            base.PerformAction(source, targets, actionFinishedCallback);
        }
    }
}