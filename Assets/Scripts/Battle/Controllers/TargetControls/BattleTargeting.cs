using BattleModule.Utility.Enums;
using System.Collections.Generic;

namespace BattleModule.Controllers.Target
{
    public abstract class BattleTargeting
    {
        protected Stack<Character> _targetedCharacters;

        protected BattleTargeting() 
        {
            _targetedCharacters = new Stack<Character>();
        }

        public abstract TargetSearchType SearchType { get; }

        public abstract Stack<Character> GetTargets(
            List<Character> targets, 
            Character selectedCharacter, 
            ref int targetsLeft);
    }

    public class AOEBattleTargeting : BattleTargeting 
    {
        public override TargetSearchType SearchType => TargetSearchType.AOE;

        public override Stack<Character> GetTargets(
            List<Character> targets,
            Character selectedCharacter,
            ref int targetsLeft)
        {
            _targetedCharacters.Clear();

            int selectedCharacterIndex = targets.IndexOf(selectedCharacter);

            _targetedCharacters.Push(selectedCharacter);

            for (int i = 0; i < targets.Count; i++) 
            {
                if (i == selectedCharacterIndex) 
                {
                    continue;
                }

                _targetedCharacters.Push(targets[i]);
            }

            targetsLeft = 0;

            return _targetedCharacters;
        }
    }

    public class SequenceBattleTargeting : BattleTargeting 
    {
        public override TargetSearchType SearchType => TargetSearchType.SEQUENCE;

        public override Stack<Character> GetTargets(
            List<Character> targets,
            Character selectedCharacter,
            ref int targetsLeft)
        {
            _targetedCharacters.Push(selectedCharacter);

            targetsLeft = targetsLeft - 1;

            return _targetedCharacters;
        }
    }
}
