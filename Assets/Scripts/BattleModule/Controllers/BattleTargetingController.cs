using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BattleModule.Controllers.Targeting.Processor;
using BattleModule.Utility.Enums;

namespace BattleModule.Controllers
{
    public class BattleTargetingController
    {
        private readonly List<Character> _charactersOnScene;

        private List<Character> _currentPossibleTargets;
        
        private Character _characterToHaveTurn;

        private Character _mainTarget;

        private int _maxTargetCount;

        public Action<Vector3> OnCharacterTargetChanged = delegate { };

        public BattleTargetingController(BattleTurnController battleTurnController)
        {
            battleTurnController.OnCharacterToHaveTurnChanged += OnCharacterToHaveTurnChanged;

            _charactersOnScene = BattleSpawner.Instance.GetSpawnedCharacters();
        }

        public void SetTargetingData(TargetType targetType, int maxTargetCount)
        {
            _maxTargetCount = maxTargetCount;
            
            SetPossibleTargets(targetType);
            
            SetMainTarget();
        }
        
        public void SetNeighbourAsMainTarget(int direction)
        {
            SetMainTarget(GetNearbyCharacterIndex(_currentPossibleTargets.IndexOf(_mainTarget) + direction, _currentPossibleTargets.Count));
        }

        public bool IsReadyForAction(ref Stack<Character> currentTargets)
        {
            return BattleTargetingProcessor.AddSelectedTargets(ref currentTargets);
        }

        private void OnCharacterToHaveTurnChanged(List<Character> charactersToHaveTurn) 
        {
            _characterToHaveTurn = charactersToHaveTurn.First();
        }

        private Func<Type, bool> GetSearchFunction(TargetType targetType) 
        {
            return (selectedCharacterType) => targetType == TargetType.ALLY ?
                selectedCharacterType == _characterToHaveTurn.GetType() : selectedCharacterType != _characterToHaveTurn.GetType();
        }

        private int GetNearbyCharacterIndex(float desiredIndex, float listSize)
        {
            return (int) (desiredIndex - listSize * Mathf.Floor(desiredIndex / listSize));
        }

        private void SetPossibleTargets(TargetType targetType) 
        {
            _currentPossibleTargets = _charactersOnScene.Where((character) => GetSearchFunction(targetType).Invoke(character.GetType())).ToList();
        }

        private void SetMainTarget(int characterIndex = -1)
        {
            _mainTarget =
                _currentPossibleTargets[characterIndex == -1 ? _currentPossibleTargets.Count / 2 : characterIndex];
            
            BattleTargetingProcessor.GetSelectedTargets(
                _currentPossibleTargets,
                _mainTarget,
                _maxTargetCount);
            
            OnCharacterTargetChanged?.Invoke(_mainTarget.transform.position);
        }
    }
}