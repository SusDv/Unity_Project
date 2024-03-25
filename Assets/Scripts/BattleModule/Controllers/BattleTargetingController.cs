using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions.BattleActions.Context;
using UnityEngine;
using BattleModule.Controllers.Targeting.Processor;
using BattleModule.Controllers.Turn;
using BattleModule.Utility.Enums;
using CharacterModule;
using VContainer;

namespace BattleModule.Controllers
{
    public class BattleTargetingController
    {
        private List<Character> _charactersOnScene;

        private readonly BattleTargetingProcessor _battleTargetingProcessor;

        private List<Character> _currentPossibleTargets;
        
        private Character _characterToHaveTurn;

        private Character _mainTarget;

        public Action<List<Character>> OnCharacterTargetChanged = delegate { };

        [Inject]
        public BattleTargetingController(BattleSpawner battleSpawner, BattleTurnController battleTurnController)
        {
            battleTurnController.OnCharactersInTurnChanged += OnCharactersInTurnChanged;

            battleSpawner.OnCharactersSpawned += OnCharactersSpawned;
            
            _battleTargetingProcessor = new BattleTargetingProcessor();
        }

        public void SetTargetingData(BattleActionContext context)
        {
            _battleTargetingProcessor.SetTargetingData(context.TargetingObject.TargetSearchType, context.TargetingObject.MaxTargetsCount);
            
            SetPossibleTargets(context.TargetingObject.TargetType);
            
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

        private void OnCharactersSpawned(List<Character> characters)
        {
            _charactersOnScene = characters;

            _characterToHaveTurn = _charactersOnScene.First();
        }

        private void OnCharactersInTurnChanged(BattleTurnContext battleTurnContext) 
        {
            _characterToHaveTurn = battleTurnContext.CharacterInAction;

            _charactersOnScene = battleTurnContext.CharactersInTurn;
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