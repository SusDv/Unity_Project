using System.Collections.Generic;
using BattleModule.Controllers.Targeting.Base;
using BattleModule.Utility.Enums;

namespace BattleModule.Controllers.Targeting
{
    public class SequenceBattleTargeting : BattleTargeting
    {
        private Character _mainTarget;

        public override TargetSearchType TargetSearchType => TargetSearchType.SEQUENCE;

        public override IEnumerable<Character> GetSelectedTargets(
            List<Character> characters, 
            Character mainTarget, 
            int maxTargetsToSelect)
        {
            return new Stack<Character>(new [] { mainTarget });
        }

        public override bool AddSelectedTargets(
            ref Stack<Character> currentTargets,
            int maxTargetsToSelect)
        {
            currentTargets.Push(_mainTarget);

            return maxTargetsToSelect == currentTargets.Count;
        }

        public override void OnCancelAction(
            ref Stack<Character> currentTargets)
        {
            currentTargets.Pop();
        }
    }
}
