using System.Collections.Generic;
using BattleModule.Utility.Enums;

namespace BattleModule.Controllers.Targeting
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
}
