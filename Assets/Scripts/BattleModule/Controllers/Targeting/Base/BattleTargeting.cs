using System.Collections.Generic;
using BattleModule.Utility.Enums;

namespace BattleModule.Controllers.Targeting.Base
{
    public abstract class BattleTargeting
    {
        protected Stack<Character> SelectedCharacters = new();
        
        public abstract TargetSearchType TargetSearchType { get; }

        public abstract IEnumerable<Character> GetSelectedTargets(
            List<Character> characters,
            Character mainTarget,
            int numberOfCharactersToSelect);

        public abstract bool AddSelectedTargets(ref Stack<Character> currentTargets, int maxTargetsToSelect);

        public abstract void OnCancelAction(ref Stack<Character> currentTargets);
    }
}
