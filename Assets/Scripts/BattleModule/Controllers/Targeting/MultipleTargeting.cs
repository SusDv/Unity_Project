using System.Collections.Generic;
using BattleModule.Controllers.Targeting.Base;
using BattleModule.Utility;
using CharacterModule;
using UnityEngine;

namespace BattleModule.Controllers.Targeting
{
    public class MultipleTargeting : BattleTargeting
    {
        public override TargetSearchType TargetSearchType => TargetSearchType.MULTIPLE_TARGET;
        
        public override void PrepareTargets(
            int mainTargetIndex)
        {
            GetNeighboursTarget(mainTargetIndex, NumberOfCharactersToSelect / 2);
        }

        private void GetNeighboursTarget(
            int mainTargetIndex,
            int charactersToPick)
        {
            SelectedCharacters = new List<Character>();
            
            var leftBound = (int) Mathf.Clamp(mainTargetIndex - charactersToPick, 0f, TargetPool.Count);

            var rightBound = (int) Mathf.Clamp(mainTargetIndex + charactersToPick, 0f, TargetPool.Count - 1);
            
            for (int i = leftBound; i <= rightBound; i++)
            {
                SelectedCharacters.Add(TargetPool[i]);
            }
        }

        public override bool AddSelectedTargets(
            ref Stack<Character> currentTargets)
        {
            foreach (var character in SelectedCharacters) 
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
