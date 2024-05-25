using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions.BattleActions.Context;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.Targeting.Processor;
using BattleModule.Utility;
using BattleModule.Utility.Interfaces;
using CharacterModule.Types.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace BattleModule.Controllers.Modules
{
    public class BattleTargetingController : IBattleCancelable, ILoadingUnit<List<Character>>
    {
        private readonly BattleCancelableController _battleCancelableController;

        private readonly BattleTurnController _battleTurnController;

        private readonly BattleActionController _battleActionController;
        
        private BattleTargetingProcessor _battleTargetingProcessor;
        

        private List<Character> _possibleTargets;
        
        private List<Character> _charactersOnScene;
        

        private int _mainTargetIndex = -1;
        
        [Inject]
        private BattleTargetingController(BattleCancelableController battleCancelableController,
            BattleTurnController battleTurnController,
            BattleActionController battleActionController)
        {
            _battleCancelableController = battleCancelableController;

            _battleActionController = battleActionController;
        }
        
        public event Action<List<Character>> OnTargetsChanged = delegate { };
        
        public void SetMainTargetWithInput(int direction)
        {
            _mainTargetIndex = GetNearbyCharacterIndex(_mainTargetIndex + direction, _possibleTargets.Count);
            
            SetMainTarget();
        }

        public void SetMainTargetWithInput(Character character)
        {
            if (!character || !_possibleTargets.Contains(character))
            {
                return;
            }

            _mainTargetIndex = _possibleTargets.IndexOf(character);
            
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
        
        public UniTask Load(List<Character> characters)
        {
            _charactersOnScene = characters;
            
            _battleActionController.OnBattleActionChanged += SetTargetingData;
            
            _battleCancelableController.AppendCancelable(this);
            
            _battleTargetingProcessor = new BattleTargetingProcessor(CharacterTargetChanged);
            
            return UniTask.CompletedTask;
        }
        
        private void SetTargetingData(BattleActionContext context)
        {
            SetPossibleTargets(context.CurrentCharacter.GetType(),
                context.BattleObject.TargetType);
            
            _battleTargetingProcessor.SetTargetingData(
                context.BattleObject.TargetSearchType, 
                _possibleTargets, 
                context.BattleObject.MaxTargetsCount);
            
            SetMainTarget();
        }
        
        private static int GetNearbyCharacterIndex(float desiredIndex, float listSize)
        {
            return (int) (desiredIndex - listSize * Mathf.Floor(desiredIndex / listSize));
        }

        private Func<Type, bool> GetSearchFunction(Type characterType, TargetType targetType)
        {
            return (selectedCharacterType) => targetType == TargetType.ALLY ?
                selectedCharacterType == characterType : selectedCharacterType != characterType;
        }

        private void SetPossibleTargets(Type characterType, TargetType targetType)
        {
            _possibleTargets = _charactersOnScene.Where((character) => GetSearchFunction(characterType, targetType).Invoke(character.GetType())).ToList();
            
            _mainTargetIndex = _possibleTargets.Count / 2;
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