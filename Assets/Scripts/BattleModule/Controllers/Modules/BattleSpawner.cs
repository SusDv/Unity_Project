using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleModule.Utility;
using CharacterModule.CharacterType.Base;
using UnityEngine;
using Utility;
using VContainer;

namespace BattleModule.Controllers.Modules
{
    public class BattleSpawner : MonoBehaviour, ILoadingUnit
    {
        [Header("Spawn Points")]
        [SerializeField]
        private BattleSpawnPoint _playerSpawnPoint;
        
        [SerializeField]
        private BattleSpawnPoint _enemySpawnPoint;
        
        private BattleTransitionData _battleTransitionData;
        
        private readonly List<Character> _spawnedCharacters = new ();
        
        public event Action<List<Character>> OnCharactersSpawned = delegate { };

        [Inject]
        private void Init(BattleTransitionData battleTransitionData)
        {
            _battleTransitionData = battleTransitionData;
        }

        public Task Load()
        {
            SpawnCharacters(_battleTransitionData.PlayerCharacters, _playerSpawnPoint);

            SpawnCharacters(_battleTransitionData.EnemyCharacters, _enemySpawnPoint);
            
            OnCharactersSpawned?.Invoke(_spawnedCharacters);

            return Task.CompletedTask;
        }

        public List<Character> GetSpawnedCharacters()
        {
            return _spawnedCharacters;
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