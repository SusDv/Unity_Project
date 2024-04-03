using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions.BattleActions.Context;
using UnityEngine;
using BattleModule.Controllers.Targeting.Processor;
using BattleModule.Controllers.Turn;
using BattleModule.Utility;
using CharacterModule;
using VContainer;

namespace BattleModule.Controllers
{
    public class BattleTargetingController
    {
        private readonly BattleTargetingProcessor _battleTargetingProcessor;
        
        private List<Character> _charactersOnScene;

        private List<Character> _currentPossibleTargets;
        
        private Character _characterToHaveTurn;

        private Character _mainTarget;

        public event Action<List<Character>> OnCharacterTargetChanged = delegate { };

        [Inject]
        public BattleTargetingController(BattleSpawner battleSpawner, BattleTurnController battleTurnController)
        {
            battleTurnController.OnCharactersInTurnChanged += OnCharactersInTurnChanged;

            battleSpawner.OnCharactersSpawned += OnCharactersSpawned;
            
            _battleTargetingProcessor = new BattleTargetingProcessor();
        }

        public void SetTargetingData(BattleActionContext context)
        {
            SetPossibleTargets(context.TargetingObject.TargetType);
            
            _battleTargetingProcessor.SetTargetingData(context.TargetingObject.TargetSearchType, 
                _currentPossibleTargets, context.TargetingObject.MaxTargetsCount);
            
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
            bool readyForAction = _battleTargetingProcessor.AddSelectedTargets(ref currentTargets);
            
            OnCharacterTargetChanged?.Invoke(_battleTargetingProcessor.PreviewTargetList());
            
            return readyForAction;
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
            
            _battleTargetingProcessor.PrepareTargets(_currentPossibleTargets.IndexOf(_mainTarget));
            
            OnCharacterTargetChanged?.Invoke(_battleTargetingProcessor.PreviewTargetList());
        }
    }
}