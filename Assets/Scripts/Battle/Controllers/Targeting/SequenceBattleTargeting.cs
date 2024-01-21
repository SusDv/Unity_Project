using System.Collections.Generic;
using BattleModule.Utility.Enums;

namespace BattleModule.Controllers.Targeting
{
    public class SequenceBattleTargeting : BattleTargeting 
    {
        private Character _mainTarget;

        public override TargetSearchType TargetSearchType => TargetSearchType.SEQUENCE;

        public override Stack<Character> GetSelectedTargets(
            List<Character> characters, 
            Character mainTarget, 
            int numberOfCharactersToSelect)
        {
            _selectedCharacters = new Stack<Character>();

            _mainTarget = mainTarget;

            _selectedCharacters.Push(mainTarget);

            return _selectedCharacters;
        }

        public override void AddSelectedTargets(
            ref Stack<Character> currentTargets)
        {
            currentTargets.Push(_mainTarget);
        }
    }
}
