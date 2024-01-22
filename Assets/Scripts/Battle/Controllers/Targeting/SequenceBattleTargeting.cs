using System.Collections.Generic;
using BattleModule.Utility.Enums;

namespace BattleModule.Controllers.Targeting
{
    public class SequenceBattleTargeting : BattleTargeting 
    {
        private Character _mainTarget;

        private int _numberOfCharactersToSelect;

        public override TargetSearchType TargetSearchType => TargetSearchType.SEQUENCE;

        public override Stack<Character> GetSelectedTargets(
            List<Character> characters, 
            Character mainTarget, 
            int numberOfCharactersToSelect)
        {
            _selectedCharacters = new Stack<Character>();

            _mainTarget = mainTarget;

            _selectedCharacters.Push(mainTarget);

            _numberOfCharactersToSelect = numberOfCharactersToSelect;

            return _selectedCharacters;
        }

        public override bool AddSelectedTargets(
            ref Stack<Character> currentTargets)
        {
            currentTargets.Push(_mainTarget);

            return _numberOfCharactersToSelect == currentTargets.Count;
        }

        public override void OnCancelAction(
            ref Stack<Character> currentTargets)
        {
            currentTargets.Pop();
        }
    }
}
