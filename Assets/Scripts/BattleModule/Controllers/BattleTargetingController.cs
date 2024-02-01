using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions.BattleActions.Context;
using UnityEngine;
using BattleModule.Controllers.Targeting.Processor;
using BattleModule.Utility.Enums;

namespace BattleModule.Controllers
{
    public class BattleTargetingController
    {
        private readonly List<Character> _charactersOnScene;

        private readonly BattleTargetingProcessor _battleTargetingProcessor;

        private List<Character> _currentPossibleTargets;
        
        private Character _characterToHaveTurn;

        private Character _mainTarget;

        public Action<List<Character>> OnCharacterTargetChanged = delegate { };

        public BattleTargetingController(BattleTurnController battleTurnController)
        {
            battleTurnController.OnCharacterToHaveTurnChanged += OnCharacterToHaveTurnChanged;

            _charactersOnScene = BattleSpawner.Instance.GetSpawnedCharacters();

            _battleTargetingProcessor = new BattleTargetingProcessor();
        }

        public void SetTargetingData(BattleActionContext context)
        {
            _battleTargetingProcessor.SetTargetingData(context.TargetSearchType, context.MaxTargetCount);
            
            SetPossibleTargets(context.TargetType);
            
            SetMainTarget();
        }
        
        public void SetMainTargetWithInput(int direction)
        {
            SetMainTarget(GetNearbyCharacterIndex(_currentPossibleTargets.IndexOf(_mainTarget) + direction, _currentPossibleTargets.Count));
        }

        public void SetMainTargetWithInput(Character character)
        {
            if (!character || !_currentPossibleTargets.Contains(character))
            {
                return;
            }

            SetMainTarget(_currentPossibleTargets.IndexOf(character));
        }

        public bool IsReadyForAction(ref Stack<Character> currentTargets)
        {
            return _battleTargetingProcessor.AddSelectedTargets(ref currentTargets);
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
            
            OnCharacterTargetChanged?.Invoke(_battleTargetingProcessor.GetSelectedTargets(
                _currentPossibleTargets,
                _mainTarget).ToList());
        }
    }
}