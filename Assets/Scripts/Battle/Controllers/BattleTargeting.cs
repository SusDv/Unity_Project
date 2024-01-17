using BattleModule.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle.Controllers
{
    public abstract class BattleTargeting
    {
        protected Stack<Character> _selectedCharacters;

        public abstract TargetSearchType TargetSearchType { get; }

        public abstract Stack<Character> GetSelectedTargets(
            List<Character> characters,
            Character mainTarget,
            int numberOfCharactersToSelect);

        public abstract void AddSelectedTargets(ref Stack<Character> currentTargets);
    }

    public class AOEBattleTargeting : BattleTargeting 
    {
        public override TargetSearchType TargetSearchType => TargetSearchType.AOE;

        public override Stack<Character> GetSelectedTargets(
            List<Character> characters,
            Character mainTarget, 
            int numberOfCharactersToSelect)
        {
            int mainTargetIndex = characters.IndexOf(mainTarget);

            int charactersToPick = numberOfCharactersToSelect / 2;

            GetNeighboursTarget(characters, mainTargetIndex, charactersToPick, out _selectedCharacters);

            _selectedCharacters.Push(mainTarget);

            return _selectedCharacters;
        }

        private void GetNeighboursTarget(
            List<Character> characters,
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

        public override void AddSelectedTargets(
            ref Stack<Character> currentTargets)
        {
            foreach (Character character in _selectedCharacters.Reverse()) 
            {
                currentTargets.Push(character);
            }
        }
    }

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
