using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace BattleModule.Controllers
{
    public class BattleSpawner : MonoBehaviour
    {
        [Header("Spawn Points")]
        [SerializeField] private Transform _playerSpawnBoxPoint;
        [SerializeField] private Transform _enemySpawnBoxPoint;

        [SerializeField]
        [Range(2f, 15f)]
        private float _distanceBetweenCharacters = 2f;

        private readonly List<Character> _spawnedCharacters = new List<Character>();

        public void SpawnCharacters()
        {
            SpawnCharactersByType(typeof(Player));

            SpawnCharactersByType(typeof(Enemy));
        }

        private void SpawnCharactersByType(Type characterType) 
        {
            var distanceToMoveBox = 0f;
            
            var spawnPoint = GetSpawnPoint(characterType);

            var charactersToSpawn = BattleManager.Instance.CharactersToSpawn.Where((character) => character.GetType() == characterType).ToList();

            for (var i = 0; i < charactersToSpawn.Count; i++)
            {
                var spawnPosition = spawnPoint.position +
                                    (_distanceBetweenCharacters) * i * Vector3.right;

                var character = Instantiate(
                    charactersToSpawn[i],
                    spawnPosition,
                    charactersToSpawn[i].transform.rotation,
                    spawnPoint);

                distanceToMoveBox = -(_distanceBetweenCharacters / 2f) * i;

                character.gameObject.name = $"{(character is Enemy ? "Enemy" : "Player")} {i + 1}";
               
                _spawnedCharacters.Add(character);
            }

            spawnPoint.transform.Translate(
                Vector3.right * distanceToMoveBox);
        }

        private Transform GetSpawnPoint(Type characterToSpawnType)
        {
            return characterToSpawnType == typeof(Enemy) ? _enemySpawnBoxPoint : _playerSpawnBoxPoint;
        }

        public List<Character> GetSpawnedCharacters() 
        {
            return _spawnedCharacters;
        }
    }
}