using System;
using System.Collections.Generic;
using BattleModule.Utility;
using CharacterModule.Types.Base;

namespace BattleModule.Targeting.Base
{
    public abstract class BattleTargeting
    {
        protected List<Character> TargetPool;

        protected List<Character> SelectedCharacters;

        protected int NumberOfCharactersToSelect;
        
        public abstract TargetSearchType TargetSearchType { get; }

        public void Init(List<Character> targetPool,
            int numberOfCharactersToSelect)
        {
            TargetPool = targetPool;
            
            NumberOfCharactersToSelect = numberOfCharactersToSelect;
            
            SelectedCharacters = new List<Character>();
        }
        
        public abstract void PrepareTargets(int mainTargetIndex,
            Action<List<Character>> targetChangedCallback);

        public virtual List<Character> GetSelectedTargets(Action<List<Character>> targetChangedCallback)
        {
            targetChangedCallback?.Invoke(PreviewTargetList());
            
            return SelectedCharacters;
        }
        
        public virtual bool TargetingComplete()
        {
            return true;
        }

        public virtual bool OnCancelAction(Action<List<Character>> targetChangedCallback)
        {
            targetChangedCallback?.Invoke(PreviewTargetList());
            
            return false;
        }
        
        protected virtual List<Character> PreviewTargetList()
        {
            return SelectedCharacters;
        }
    }
}
