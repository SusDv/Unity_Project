using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Transition;
using BattleModule.Utility;
using CharacterModule;
using UnityEngine;
using Utility;
using VContainer;

namespace BattleModule.Controllers
{
    public class BattleSpawner : MonoBehaviour
    {
        [Header("Spawn Points")]
        [SerializeField]
        private BattleSpawnPoint _playerSpawnPoint;
        
        [SerializeField]
        private BattleSpawnPoint _enemySpawnPoint;
        
        private List<Character> _spawnedCharacters;

        private BattleTransitionData _battleTransitionData;

        public event Action<List<Character>> OnCharactersSpawned = delegate { };

        [Inject]
        private void Init(BattleTransitionData battleTransitionData)
        {
            _battleTransitionData = battleTransitionData;

            _spawnedCharacters = new List<Character>();
        }

        private void Start()
        {
            SpawnCharacters(_battleTransitionData.PlayerCharacters, _playerSpawnPoint);

            SpawnCharacters(_battleTransitionData.EnemyCharacters, _enemySpawnPoint);
            
            OnCharactersSpawned?.Invoke(_spawnedCharacters);
        }

        private void SpawnCharacters<T>(IEnumerable<T> characters, BattleSpawnPoint battleSpawnPoint)
            where T : Character
        {
            var spawnedCharacters = characters.Select(character => Instantiate(character, character.transform.position, character.transform.rotation, battleSpawnPoint.CharacterSpawnPoint)).Cast<Character>().ToList();

            _spawnedCharacters.AddRange(spawnedCharacters);

            battleSpawnPoint.Init(spawnedCharacters);
        }
    }
}