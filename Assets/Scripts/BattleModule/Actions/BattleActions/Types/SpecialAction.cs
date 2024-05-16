using System.Collections.Generic;
using BattleModule.Accuracy;
using BattleModule.Actions.BattleActions.Base;
using CharacterModule.CharacterType.Base;

namespace BattleModule.Actions.BattleActions.Types
{
    public class SpecialAction : BattleAction
    {
        public override void PerformAction(Character source,
            List<Character> targets, Dictionary<Character, BattleAccuracy> accuracies)
        {
            source.WeaponController.GetSpecialAttack().Attack(targets);
            
            base.PerformAction(source, targets, accuracies);
        }
    }
}