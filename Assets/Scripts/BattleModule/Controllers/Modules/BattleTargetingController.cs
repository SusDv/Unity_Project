using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions.Context;
using BattleModule.Targeting.Base;
using BattleModule.Utility;
using BattleModule.Utility.Interfaces;
using CharacterModule.Types.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;
using VContainer;

namespace BattleModule.Controllers.Modules
{
    public class BattleTargetingController : IBattleCancelable, ILoadingUnit<List<Character>>
    {
        private readonly BattleCancelableController _battleCancelableController;

        private readonly BattleActionController _battleActionController;

        private BattleTargeting _battleTargeting;
        
        
        private readonly Dictionary<TargetSearchType, BattleTargeting> _targetingTypes = new();
        
        private List<Character> _possibleTargets;
        
        private List<Character> _charactersOnScene;

        private int _mainTargetIndex = -1;
        
        [Inject]
        private BattleTargetingController(BattleCancelableController battleCancelableController,
            BattleActionController battleActionController)
        {
            _battleCancelableController = battleCancelableController;

            _battleActionController = battleActionController;
        }
        
        public event Action<List<Character>> OnTargetsChanged = delegate { };

        public void ResetTargetIndex()
        {
            _mainTargetIndex = -1;
        }

        public void SetMainTargetWithInput(int direction)
        {
            _mainTargetIndex = GetNearbyCharacterIndex(_mainTargetIndex + direction, _possibleTargets.Count);
            
            SetMainTarget();
        }

        public void SetMainTargetWithInput(Character character)
        {
            if (!character)
            {
                return;
            }

            _mainTargetIndex = _possibleTargets.IndexOf(character);
            
            SetMainTarget();
        }

        public bool IsReadyForAction()
        {
            return _battleTargeting.TargetingComplete();
        }

        public List<Character> GetSelectedTargets()
        {
            return _battleTargeting.GetSelectedTargets();
        }

        public bool TryCancel()
        {
            return _battleTargeting.OnCancelAction();
        }
        
        public UniTask Load(List<Character> characters)
        {
            _charactersOnScene = characters;
            
            _battleActionController.OnBattleActionChanged += SetTargetingData;
            
            _battleCancelableController.TryAppendCancelable(this);
            
            InitTargetingTypes();
            
            return UniTask.CompletedTask;
        }

        private void InitTargetingTypes()
        {
            _targetingTypes.Clear();
            
            foreach(var targeting in ReflectionUtils.GetConcreteInstances<BattleTargeting>()) 
            {
                _targetingTypes.Add(targeting.TargetSearchType, targeting);
            }
        }

        private void SetTargetingData(BattleActionContext context)
        {
            SetPossibleTargets(context.BattleObject.TargetType,
                context.CharacterInActionType);
            
            SetTargetingType(context.BattleObject.TargetSearchType,
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

        private void SetPossibleTargets(TargetType targetType, Type characterInActionType)
        {
            _possibleTargets = _charactersOnScene.Where((character) => GetSearchFunction(characterInActionType, targetType).Invoke(character.GetType())).ToList();

            _mainTargetIndex = GetMainTargetIndex();
        }

        private void SetTargetingType(TargetSearchType targetSearchType,
            int maxTargetsCount)
        {
            _battleTargeting = _targetingTypes[targetSearchType];
            
            _battleTargeting.Init(_possibleTargets, maxTargetsCount, CharacterTargetChanged);
        }

        private int GetMainTargetIndex()
        {
            return _mainTargetIndex == -1 ? _possibleTargets.Count / 2 : _mainTargetIndex;
        }

        private void SetMainTarget()
        {
            _battleTargeting.PrepareTargets(_mainTargetIndex);
        }

        private void CharacterTargetChanged(List<Character> selectedCharacters)
        {
            OnTargetsChanged?.Invoke(selectedCharacters);
        }
    }
}