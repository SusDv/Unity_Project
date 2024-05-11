using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleModule.Actions.BattleActions.Context;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.Input;
using BattleModule.Targeting.Processor;
using BattleModule.Utility;
using BattleModule.Utility.Interfaces;
using CharacterModule.CharacterType.Base;
using UnityEngine;
using VContainer;

namespace BattleModule.Controllers.Modules
{
    public class BattleTargetingController : IBattleCancelable, ILoadingUnit
    {
        private readonly BattleInput _battleInput;

        private readonly BattleTurnController _battleTurnController;

        private readonly BattleActionController _battleActionController;
        
        
        private BattleTargetingProcessor _battleTargetingProcessor;

        private List<Character> _currentPossibleTargets;

        private BattleTurnContext _battleTurnContext;

        private int _mainTargetIndex = -1;
        
        [Inject]
        private BattleTargetingController(BattleInput battleInput,
            BattleTurnController battleTurnController,
            BattleActionController battleActionController)
        {
            _battleInput = battleInput;
            
            _battleTurnController = battleTurnController;

            _battleActionController = battleActionController;
        }
        
        public event Action<List<Character>> OnTargetsChanged = delegate { };
        
        public void SetMainTargetWithInput(int direction)
        {
            _mainTargetIndex = GetNearbyCharacterIndex(_mainTargetIndex + direction, _currentPossibleTargets.Count);
            
            SetMainTarget();
        }

        public void SetMainTargetWithInput(Character character)
        {
            if (!character || !_currentPossibleTargets.Contains(character))
            {
                return;
            }

            _mainTargetIndex = _currentPossibleTargets.IndexOf(character);
            
            SetMainTarget();
        }

        public bool IsReadyForAction()
        {
            return _battleTargetingProcessor.TargetingComplete();
        }

        public List<Character> GetSelectedTargets()
        {
            return _battleTargetingProcessor.GetSelectedTargets();
        }

        public bool Cancel()
        {
            return _battleTargetingProcessor.CancelAction();
        }
        
        public Task Load()
        {
            _battleTurnController.OnCharactersInTurnChanged += OnCharactersInTurnChanged;

            _battleActionController.OnBattleActionChanged += SetTargetingData;
            
            _battleInput.AppendCancelable(this);
            
            _battleTargetingProcessor = new BattleTargetingProcessor(CharacterTargetChanged);
            
            return Task.CompletedTask;
        }
        
        private void SetTargetingData(BattleActionContext context)
        {
            SetPossibleTargets(context.BattleObject.TargetType);
            
            _battleTargetingProcessor.SetTargetingData(
                context.BattleObject.TargetSearchType, 
                _currentPossibleTargets, 
                context.BattleObject.MaxTargetsCount);
            
            SetMainTarget();
        }
        
        private static int GetNearbyCharacterIndex(float desiredIndex, float listSize)
        {
            return (int) (desiredIndex - listSize * Mathf.Floor(desiredIndex / listSize));
        }

        private void OnCharactersInTurnChanged(BattleTurnContext battleTurnContext)
        {
            _battleTurnContext = battleTurnContext;
        }

        private Func<Type, bool> GetSearchFunction(TargetType targetType)
        {
            var characterToInTurnType = _battleTurnContext.CharacterInAction.GetType();
            
            return (selectedCharacterType) => targetType == TargetType.ALLY ?
                selectedCharacterType == characterToInTurnType : selectedCharacterType != characterToInTurnType;
        }

        private void SetPossibleTargets(TargetType targetType)
        {
            _currentPossibleTargets = _battleTurnContext.CharactersInTurn.Where((character) => GetSearchFunction(targetType).Invoke(character.GetType())).ToList();
            
            _mainTargetIndex = _currentPossibleTargets.Count / 2;
        }

        private void SetMainTarget()
        {
            _battleTargetingProcessor.PrepareTargets(_mainTargetIndex);
        }

        private void CharacterTargetChanged(List<Character> selectedCharacters)
        {
            OnTargetsChanged?.Invoke(selectedCharacters);
        }
    }
}