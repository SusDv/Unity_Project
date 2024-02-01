using System.Collections.Generic;
using System.Linq;
using BattleModule.Controllers.Targeting.Base;
using BattleModule.Utility.Enums;

namespace BattleModule.Controllers.Targeting
{
    public class AoeBattleTargeting : BattleTargeting 
    {
        public override TargetSearchType TargetSearchType => TargetSearchType.AOE;

        public override Stack<Character> GetSelectedTargets(
            List<Character> characters,
            Character mainTarget, 
            int numberOfCharactersToSelect)
        {
            int mainTargetIndex = characters.IndexOf(mainTarget);

            int charactersToPick = numberOfCharactersToSelect / 2;

            GetNeighboursTarget(characters, mainTargetIndex, charactersToPick, out SelectedCharacters);

            SelectedCharacters.Push(mainTarget);

            return SelectedCharacters;
        }

        private void GetNeighboursTarget(
            IReadOnlyList<Character> characters,
            int mainTargetIndex,
            int charactersToPick,
            out Stack<Character> selectedCharacters)
        {
            selectedCharacters = new Stack<Character>();

            for (
                int i = mainTargetIndex + 1,
                j = charactersToPick; i < characters.Count && j > 0; i++, j--)
            {
                selectedCharacters.Push(characters[i]);
            }

            for (
                int i = mainTargetIndex - 1,
                j = charactersToPick; i >= 0 && j > 0; i--, j--)
            {
                selectedCharacters.Push(characters[i]);
            }
        }

        public override bool AddSelectedTargets(
            ref Stack<Character> currentTargets,
            int maxTargetsToSelect)
        {
            foreach (var character in SelectedCharacters.Reverse()) 
            {
                currentTargets.Push(character);
            }

            return true;
        }

        public override void OnCancelAction(
            ref Stack<Character> currentTargets)
        { }
    }
}
