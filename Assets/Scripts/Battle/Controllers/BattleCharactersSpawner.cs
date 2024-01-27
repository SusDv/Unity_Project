using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace BattleModule.Controllers
{
    public class BattleCharactersSpawner : MonoBehaviour
    {
        [Header("Spawn Points")]
        [SerializeField] private Transform _playerSpawnBoxPoint;
        [SerializeField] private Transform _enemySpawnBoxPoint;

        [SerializeField]
        [Range(2f, 15f)]
        private float _distanceBetweenCharacters = 2f;

        private readonly List<Character> _spawnedCharacters;

        public BattleCharactersSpawner() 
        {
            _spawnedCharacters = new List<Character>();
        }

        public void SpawnCharacters()
        {
            SpawnSpecificCharacters(typeof(Player));

            SpawnSpecificCharacters(typeof(Enemy));
        }

        public List<Character> GetSpawnedCharacters() 
        {
            return _spawnedCharacters;
        }

        private void SpawnSpecificCharacters(Type characterType) 
        {
            float distanceToMoveBox = 0f;
            
            Transform spawnPoint = GetSpawnedCharacterInfo(characterType);

            List<Character> charactersToSpawn = BattleManager.Instance.CharactersToSpawn.Where((character) => character.GetType().Equals(characterType)).ToList();

            for (int i = 0; i < charactersToSpawn.Count; i++)
            {
                
                Vector3 spawnPosition = spawnPoint.position +
                    (_distanceBetweenCharacters) * i * Vector3.right;

                Character character = Instantiate(
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

        private Transform GetSpawnedCharacterInfo(Type characterToSpawnType)
        {
            return characterToSpawnType.Equals(typeof(Enemy)) ? _enemySpawnBoxPoint : _playerSpawnBoxPoint;
        }
    }
}