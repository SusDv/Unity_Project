using BattleModule.ActionCore.Events;
using BattleModule.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleModule.Controllers
{
    public class BattleCharactersOnScene
    {
        private readonly List<Character> _charactersOnScene;

        private Character _characterToHaveTurn;

        public Action<Vector3> OnCharacterTargetChanged = delegate { };

        public BattleCharactersOnScene(BattleTurnController battleTurnController)
        {
            battleTurnController.OnCharacterToHaveTurnChanged += OnCharacterToHaveTurnChanged;

            _charactersOnScene = BattleSpawner.Instance.GetSpawnedCharacters();
        }

        private void OnCharacterToHaveTurnChanged(List<Character> charactersToHaveTurn) 
        {
            _characterToHaveTurn = charactersToHaveTurn.First();
        }

        private Func<Type, bool> GetSearchFunction(TargetType targetType) 
        {
            return (selectedCharacterType) => targetType == TargetType.ALLY ?
                            selectedCharacterType.Equals(_characterToHaveTurn.GetType()) : !selectedCharacterType.Equals(_characterToHaveTurn.GetType());
        }

        private int GetNearbyCharacterIndex(float desiredIndex, float listSize)
        {
            return (int) (desiredIndex - listSize * Mathf.Floor(desiredIndex / listSize));
        }

        public List<Character> GetCharactersByType(TargetType targetType) 
        {
            return _charactersOnScene.Where((character) => GetSearchFunction(targetType).Invoke(character.GetType())).ToList();
        }

        public Character GetCharacterOnScene(
            TargetType targetType,
            int characterIndex)
        {
            List<Character> characters = GetCharactersByType(targetType);

            Character selectedCharacter = characters[characterIndex == -1 ?
                Mathf.RoundToInt(characters.Count / 2) :
                characterIndex];

            OnCharacterTargetChanged?.Invoke(selectedCharacter.transform.position);

            return selectedCharacter;
        }

        public int GetNearbyCharacterOnSceneIndex(Character selectedCharacter, int direction)
        {
            List<Character> characters =
                _charactersOnScene
                    .Where(character => character.GetType().Equals(selectedCharacter.GetType()))
                        .ToList();

            int nearbyCharacterIndex = GetNearbyCharacterIndex(characters.IndexOf(selectedCharacter) + direction, characters.Count);
            
            return nearbyCharacterIndex;
        }
    }
}