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

        protected Action<List<Character>> TargetChangedCallback = delegate { };
        
        public abstract TargetSearchType TargetSearchType { get; }

        public void Init(List<Character> targetPool,
            int numberOfCharactersToSelect,
            Action<List<Character>> targetChangedCallback)
        {
            TargetPool = targetPool;
            
            NumberOfCharactersToSelect = numberOfCharactersToSelect;
            
            SelectedCharacters = new List<Character>();

            TargetChangedCallback = targetChangedCallback;
        }
        
        public abstract void PrepareTargets(int mainTargetIndex);

        public virtual List<Character> GetSelectedTargets()
        {
            TargetChangedCallback?.Invoke(PreviewTargetList());
            
            return SelectedCharacters;
        }
        
        public virtual bool TargetingComplete()
        {
            return true;
        }

        public virtual bool OnCancelAction()
        {
            TargetChangedCallback?.Invoke(PreviewTargetList());
            
            return false;
        }
        
        protected virtual List<Character> PreviewTargetList()
        {
            return SelectedCharacters;
        }
    }
}
