using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleModule.Controllers
{
    public class BattleCharactersOnScene
    {
        private List<Character> _charactersOnScene;

        public BattleCharactersOnScene()
        {
            _charactersOnScene = new List<Character>();
        }

        private int GetNearbyCharacterIndex(float desiredIndex, float listSize)
        {
            return (int) (desiredIndex - listSize * Mathf.Floor(desiredIndex / listSize));
        }

        public void AddCharactersOnScene(List<Character> characters)
        {
            foreach (Character character in characters)
            {
                _charactersOnScene.Add(character);
            }
        }

        public Character GetMiddleCharacterOnScene(Type characterInTurnType, Func<Type, Type, bool> targetFunction, int specificTargetIndex = -1)
        {
            List<Character> characters =
                _charactersOnScene
                    .Where((character) => 
                    targetFunction.Invoke(
                        character.GetType(), characterInTurnType))
                            .ToList();

            return characters[specificTargetIndex == -1 ? Mathf.RoundToInt(characters.Count / 2) : specificTargetIndex];
        }

        public (int, Character) GetNearbyCharacterOnScene(Character selectedCharacter, int direction)
        {
            List<Character> characters =
                _charactersOnScene
                    .Where(character => character.GetType().Equals(selectedCharacter.GetType()))
                        .ToList();

            int nearbyCharacterIndex = GetNearbyCharacterIndex(characters.IndexOf(selectedCharacter) + direction, characters.Count);

            return (nearbyCharacterIndex, characters[nearbyCharacterIndex]);
        }

        public List<Character> GetCharactersOnScene()
        {
            return _charactersOnScene;
        }
    }
}