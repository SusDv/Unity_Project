using System.Collections.Generic;
using System.Linq;
using BattleModule.Controllers.Targeting.Base;
using BattleModule.Utility;
using CharacterModule;

namespace BattleModule.Controllers.Targeting
{
    public class SequenceTargeting : BattleTargeting
    {
        private int _mainTargetIndex;
        public override TargetSearchType TargetSearchType => TargetSearchType.SEQUENCE;

        public override void PrepareTargets(int mainTargetIndex)
        {
            _mainTargetIndex = mainTargetIndex;
        }

        public override List<Character> PreviewTargetList()
        {
            var previewList = SelectedCharacters.ToList();

            previewList.Add(TargetPool[_mainTargetIndex]);

            return previewList;
        }

        public override bool AddSelectedTargets(
            ref Stack<Character> currentTargets)
        {
            SelectedCharacters.Add(TargetPool[_mainTargetIndex]);
            
            if (SelectedCharacters.Count < NumberOfCharactersToSelect)
            { 
                return false;
            }

            foreach (var target in SelectedCharacters)
            {
                currentTargets.Push(target);
            }
            
            return true;
        }

        public override void OnCancelAction(
            ref Stack<Character> currentTargets)
        {
            currentTargets.Pop();

            SelectedCharacters = currentTargets.ToList();
        }
    }
}
