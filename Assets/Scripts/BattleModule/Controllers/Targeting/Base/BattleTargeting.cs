using System.Collections.Generic;
using BattleModule.Utility;
using CharacterModule;

namespace BattleModule.Controllers.Targeting.Base
{
    public abstract class BattleTargeting
    {
        protected List<Character> TargetPool = new();

        protected List<Character> SelectedCharacters = new();

        protected int NumberOfCharactersToSelect;
        
        public abstract TargetSearchType TargetSearchType { get; }

        public void Init(List<Character> targetPool,
            int numberOfCharactersToSelect)
        {
            TargetPool = targetPool;
            NumberOfCharactersToSelect = numberOfCharactersToSelect;
        }

        public virtual List<Character> PreviewTargetList()
        {
            return SelectedCharacters;
        }

        public abstract void PrepareTargets(int mainTargetIndex);

        public abstract bool AddSelectedTargets(
            ref Stack<Character> currentTargets);

        public abstract void OnCancelAction(ref Stack<Character> currentTargets);
    }
}
