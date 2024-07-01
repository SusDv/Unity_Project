using System;
using System.Collections.Generic;
using System.Linq;
using CharacterModule.Types;
using CharacterModule.Types.Base;
using CharacterModule.Utility;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;
using VContainer;

namespace BattleModule.Controllers.Modules.Turn
{
    public class BattleTurnController : ILoadingUnit<List<Character>>
    {
        private readonly BattleTimerController _battleTimerController;

        private readonly BattleTurnEvents _battleTurnEvents;

        private readonly BattleDeathController _battleDeathController;
        
        private List<Character> _spawnedCharacters;

        public bool IsNextEnemy => _spawnedCharacters.First() is Enemy;
        
        public event Action<BattleTurnContext> OnCharactersInTurnChanged = delegate { };
        
        [Inject]
        public BattleTurnController(BattleTimerController battleTimerController,
            BattleTurnEvents battleTurnEvents,
            BattleDeathController battleDeathController)
        {
            _battleTimerController = battleTimerController;

            _battleTurnEvents = battleTurnEvents;

            _battleDeathController = battleDeathController;
        }
        
        private void UpdateCharactersBattlePoints()
        {
            foreach (var character in _spawnedCharacters)
            {
                float battlePoints = character.Stats.GetStatInfo(StatType.BATTLE_POINTS).FinalValue;
                
                character.Stats.ApplyInstantModifier(StatType.BATTLE_POINTS, CalculateDeduction(battlePoints));
            }

            SortCharacters();
        }

        private static int CalculateDeduction(float battlePoints)
        {
            var tierNumber = (int) Mathf.Clamp(battlePoints / 10f, 0f, 4f);

            return -(battlePoints <= 10 * tierNumber ? 2 * tierNumber
                : battlePoints < 10 * tierNumber + 2 * tierNumber
                    ? 3 + 2 * (tierNumber - 1)
                    : 4 + 2 * (tierNumber - 1));
        }

        private void ResetFirstCharacterBattlePoints()
        {
            _spawnedCharacters.First().Stats.ResetStatValue(StatType.BATTLE_POINTS);
            
            OnCharactersInTurnChanged?.Invoke(GetBattleTurnContext());
        }

        private void OnCharacterDied(GameObject deadCharacter)
        {
            _spawnedCharacters.Select(c => c.gameObject).ToList().Remove(deadCharacter);

            _battleTimerController.SetLocalCycle(_spawnedCharacters.Count);

            OnCharactersInTurnChanged?.Invoke(GetBattleTurnContext());
        }
        
        private void SortCharacters()
        {
            _spawnedCharacters = _spawnedCharacters.OrderBy((character) => character.Stats.GetStatInfo(StatType.BATTLE_POINTS).FinalValue).ToList();
        }

        private void OnBattleInit()
        {
            SortCharacters();
            
            OnCharactersInTurnChanged?.Invoke(GetBattleTurnContext());
        }

        private BattleTurnContext GetBattleTurnContext()
        {
            return new BattleTurnContext
            {
                CharactersInTurn = _spawnedCharacters
            };
        }

        public void BeginTurn()
        {
            _spawnedCharacters.First().Stats.TriggerSealEffects();
            
            ResetFirstCharacterBattlePoints();
        }

        public UniTask Load(List<Character> characters)
        {
            _spawnedCharacters = characters;
            
            _spawnedCharacters.ForEach(character =>
            {
                character.Stats.SetBattleTimerFactory(_battleTimerController.CreateTimer);
            });
            
            _battleTimerController.SetLocalCycle(characters.Count);

            _battleTurnEvents.OnTurnEnd += UpdateCharactersBattlePoints;

            _battleTurnEvents.OnBattleInit += OnBattleInit;

            _battleDeathController.OnCharacterDied += OnCharacterDied;
            
            return UniTask.CompletedTask;
        }
    }
}