using BattleModule.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleModule.Controllers
{
    public class BattleCharactersOnScene
    {
        private List<Character> _charactersOnScene;

        private readonly Dictionary<TargetType, Func<Type, Type, bool>>
            _characterSearchFuntions;

        public BattleCharactersOnScene()
        {
            _charactersOnScene = new List<Character>();

            _characterSearchFuntions
                = new Dictionary<TargetType, Func<Type, Type, bool>>
                {
                    { TargetType.ALLY,
                        (selectedCharacterType, characterInTurnType) =>
                            selectedCharacterType.Equals(characterInTurnType)
                    },
                    { TargetType.ENEMY,
                        (selectedCharacterType, characterInTurnType) =>
                            !selectedCharacterType.Equals(characterInTurnType)
                    }
                };
        }

        private int GetNearbyCharacterIndex(float desiredIndex, float listSize)
        {
            return (int) (desiredIndex - listSize * Mathf.Floor(desiredIndex / listSize));
        }

        public List<Character> GetCharactersByType(Type characterInTurnType, TargetType targetType) 
        {
            return _charactersOnScene
                    .Where((character) =>
                    _characterSearchFuntions[targetType].Invoke(
                        character.GetType(), characterInTurnType))
                            .ToList();
        }

        public Character GetCharacterOnScene(
            Type characterInTurnType,
            TargetType targetType,
            int characterIndex)
        {
            List<Character> characters = GetCharactersByType(characterInTurnType, targetType);

            return characters[characterIndex == -1 ? 
                Mathf.RoundToInt(characters.Count / 2) :
                characterIndex];
        }

        public void AddCharactersOnScene(List<Character> characters)
        {
            foreach (Character character in characters)
            {
                _charactersOnScene.Add(character);
            }
        }

        public int GetNearbyCharacterOnSceneIndex(Character selectedCharacter, int direction)
        {
            List<Character> characters =
                _charactersOnScene
                    .Where(character => character.GetType().Equals(selectedCharacter.GetType()))
                        .ToList();

            return GetNearbyCharacterIndex(characters.IndexOf(selectedCharacter) + direction, characters.Count);
        }

        public List<Character> GetCharactersOnScene()
        {
            return _charactersOnScene;
        }
    }
}