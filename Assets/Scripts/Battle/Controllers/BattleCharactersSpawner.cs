using System;
using System.Collections.Generic;
using UnityEngine;

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


        private List<Character> _spawnedCharacters;

        public List<Character> SpawnCharacters(List<Character> charactersToSpawn)
        {
            var characterInfo = GetSpawnedCharacterInfo(charactersToSpawn[0]);

            _spawnedCharacters = new List<Character>();

            float distanceToMoveBox = 0f;

            for (int i = 0; i < charactersToSpawn.Count; i++)
            {
                Vector3 spawnPosition = characterInfo.spawnPoint.position +
                    (_distanceBetweenCharacters) * i * Vector3.right;

                Character character = Instantiate(
                    charactersToSpawn[i], 
                    spawnPosition, 
                    charactersToSpawn[i].transform.rotation,
                    characterInfo.spawnPoint);


                distanceToMoveBox = -(_distanceBetweenCharacters / 2f) * i;

                character.gameObject.name = $"{(character is Enemy ? "Enemy" : "Player")} {i + 1}";

                _spawnedCharacters.Add(character);
            }

            characterInfo.spawnPoint.transform.Translate(
                Vector3.right * distanceToMoveBox);

            return _spawnedCharacters;
        }
        private (bool isEnemy, Transform spawnPoint) GetSpawnedCharacterInfo(Character characterToSpawn)
        {
            bool isEnemy = characterToSpawn is Enemy;

            return (isEnemy, isEnemy ? _enemySpawnBoxPoint : _playerSpawnBoxPoint);
        }

    }
}