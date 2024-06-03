using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Targeting.Base;
using BattleModule.Utility;
using CharacterModule.Types.Base;

namespace BattleModule.Targeting
{
    public class SequenceTargeting : BattleTargeting
    {
        private int _mainTargetIndex;
        public override TargetSearchType TargetSearchType => TargetSearchType.SEQUENCE;

        public override void PrepareTargets(int mainTargetIndex,
            Action<List<Character>> targetChangedCallback)
        {
            _mainTargetIndex = mainTargetIndex;
            
            targetChangedCallback?.Invoke(PreviewTargetList());
        }

        protected override List<Character> PreviewTargetList()
        {
            var previewList = SelectedCharacters.ToList();

            // If all possible characters selected
            // We don't want to preview next target
            if (previewList.Count == NumberOfCharactersToSelect)
            {
                return previewList;
            }

            previewList.Add(TargetPool[_mainTargetIndex]);

            return previewList;
        }

        public override List<Character> GetSelectedTargets(Action<List<Character>> targetChangedCallback)
        {
            SelectedCharacters.Add(TargetPool[_mainTargetIndex]);
            
            targetChangedCallback?.Invoke(PreviewTargetList());
            
            return SelectedCharacters;
        }

        public override bool TargetingComplete()
        {
            return SelectedCharacters.Count == NumberOfCharactersToSelect;
        }

        public override bool OnCancelAction(Action<List<Character>> targetChangedCallback)
        {
            if (SelectedCharacters.Count == 0)
            {
                return true;
            }

            SelectedCharacters.RemoveAt(SelectedCharacters.Count - 1);

            targetChangedCallback?.Invoke(PreviewTargetList());
            
            return false;
        }
    }
}
