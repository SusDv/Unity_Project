using System;
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
            int mainTargetIndex,
            Action<List<Character>> targetChangedCallback)
        {
            GetNeighboursTarget(mainTargetIndex, NumberOfCharactersToSelect / 2);
            
            targetChangedCallback?.Invoke(PreviewTargetList());
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
    }
}
