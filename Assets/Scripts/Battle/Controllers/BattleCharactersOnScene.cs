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

        public Character GetMiddleTargetOnScene(Type characterInTurnType, Func<Type, Type, bool> targetFunction)
        {
            List<Character> characters =
                _charactersOnScene
                    .Where((character) => 
                    targetFunction.Invoke(
                        character.GetType(), characterInTurnType))
                            .ToList();

            return characters[Mathf.RoundToInt(characters.Count / 2)];
        }

        public Character GetNearbyCharacter(Character selectedCharacter, int direction)
        {
            List<Character> characters =
                _charactersOnScene
                    .Where(character => character.GetType().Equals(selectedCharacter.GetType()))
                        .ToList();

            return characters[GetNearbyCharacterIndex(
                characters.IndexOf(selectedCharacter) + direction, characters.Count)];
        }

        public List<Character> GetCharactersOnScene()
        {
            return _charactersOnScene;
        }
    }
}