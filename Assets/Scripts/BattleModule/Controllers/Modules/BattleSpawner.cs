using System.Collections.Generic;
using System.Linq;
using BattleModule.Utility;
using CharacterModule.Types.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;
using VContainer;

namespace BattleModule.Controllers.Modules
{
    public class BattleSpawner : MonoBehaviour, ILoadingUnit
    {
        [Header("Spawn Points")]
        [SerializeField]
        private SpawnPoint _playerSpawnPoint;
        
        [SerializeField]
        private SpawnPoint _enemySpawnPoint;
        
        private BattleTransitionData _battleTransitionData;
        
        private readonly List<Character> _spawnedCharacters = new ();
        
        
        [Inject]
        private void Init(BattleTransitionData battleTransitionData)
        {
            _battleTransitionData = battleTransitionData;
        }

        public UniTask Load()
        {
            SpawnCharacters(_battleTransitionData.PlayerCharacters, _playerSpawnPoint);

            SpawnCharacters(_battleTransitionData.EnemyCharacters, _enemySpawnPoint);

            return UniTask.CompletedTask;
        }

        public List<Character> GetSpawnedCharacters()
        {
            return _spawnedCharacters;
        }

        private void SpawnCharacters<T>(IEnumerable<T> characters, 
            SpawnPoint spawnPoint)
            where T : Character
        {
            var spawnedCharacters = (from character 
                in characters let characterTransform = character.transform 
                select Instantiate(character, 
                    characterTransform.position, 
                    characterTransform.rotation, 
                    spawnPoint.CharacterSpawnPoint)).ToList();

            spawnedCharacters.ForEach(c => c.Init());

            _spawnedCharacters.AddRange(spawnedCharacters);

            spawnPoint.Init(spawnedCharacters);
        }
    }
}