using System;
using System.Collections.Generic;
using CharacterModule;
using UnityEngine;

namespace BattleModule.Utility
{
    [Serializable]
    public class BattleSpawnPoint
    {
        [field: SerializeField] 
        public Transform CharacterSpawnPoint { get; private set; }

        [SerializeField]
        [Range(1f, 5f)]
        private float _distanceBetweenCharacters;
        
        private Vector3 _defaultSpawnPointPosition;

        private List<Character> _charactersInSpawnPoint;

        public void Init(IEnumerable<Character> spawnedCharacters)
        {
            _charactersInSpawnPoint = new List<Character>(spawnedCharacters);

            _defaultSpawnPointPosition = CharacterSpawnPoint.position;
            
            _charactersInSpawnPoint.ForEach(c =>
            {
                c.HealthManager.OnCharacterDied += OnCharacterDied;
            });
            
            AdjustPosition();
        }

        private void AdjustPosition()
        {
            foreach (var character in _charactersInSpawnPoint)
            {
                int characterIndex = _charactersInSpawnPoint.Contains(character) ? _charactersInSpawnPoint.IndexOf(character) : (int)Mathf.Max(0f, _charactersInSpawnPoint.Count - 1f);

                character.transform.position =
                    CharacterSpawnPoint.position + _distanceBetweenCharacters * characterIndex * Vector3.right;

                if (characterIndex != 0)
                {
                    CharacterSpawnPoint.transform.position += (-(_distanceBetweenCharacters / 2f) * Vector3.right);
                }
            }
        }

        private void OnCharacterDied(Character character)
        {
            if (!_charactersInSpawnPoint.Contains(character))
            {
                return;
            }

            _charactersInSpawnPoint.Remove(character);
            
            ResetPosition();
            
            AdjustPosition();
        }

        private void ResetPosition()
        {
            CharacterSpawnPoint.position = _defaultSpawnPointPosition;
        }
    }
}